namespace BetterRespawn.Handlers
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using Respawning;
    using MEC;
    using System.Collections.Generic;

    class Server
    {
        Queue<Player> wait_list;
        public void OnWaitingForPlayers()
        {
            Log.Info(message: "Loaded and waiting for players...");
            wait_list = new Queue<Player>();
        }


        public void onDeath(DyingEventArgs ev)
        {
            wait_list.Enqueue(ev.Target);
        }


        public void RespawnTicketChecker(RespawningTeamEventArgs ev)
        {
            if (BetterRespawn.Instance.Config.RespawnTeams == true)
            {
                SpawnableTeamType team = ev.NextKnownTeam;
                Timing.CallDelayed(1.5f, () =>
                {
                    if (team == SpawnableTeamType.NineTailedFox)
                    {
                        if (BetterRespawn.Instance.Config.debug) { Log.Debug("Spawning ntf..."); }
                        uint tickets = Respawn.NtfTickets;
                        while (tickets > 0 && wait_list.IsEmpty() == false)
                        {
                            Player ply = wait_list.Dequeue();
                            if (ply.IsDead == true && !ply.IsOverwatchEnabled)
                            {
                                if (BetterRespawn.Instance.Config.debug) { Log.Debug($"Spawning {ply.Nickname} at {tickets} tickets"); }
                                if (tickets >= 15)
                                {
                                    ply.SetRole(RoleType.NtfCaptain);
                                }
                                else if (tickets >= 10)
                                {
                                    ply.SetRole(RoleType.NtfSergeant);
                                }
                                else
                                {
                                    ply.SetRole(RoleType.NtfPrivate);
                                }
                                Respawn.GrantTickets(team, -1);
                                break;
                            }
                            tickets--;
                        }
                    }
                    else if (team == SpawnableTeamType.ChaosInsurgency)
                    {
                        if (BetterRespawn.Instance.Config.debug) { Log.Debug("Spawning chaos..."); }
                        uint tickets = Respawn.ChaosTickets;
                        while (tickets > 0 && wait_list.IsEmpty() == false)
                        {
                            Player ply = wait_list.Dequeue();
                            if (ply.IsDead == true && !ply.IsOverwatchEnabled)
                            {
                                if (BetterRespawn.Instance.Config.debug) { Log.Debug($"Spawning {ply.Nickname} at {tickets} tickets"); }
                                if (tickets >= 15)
                                {
                                    ply.SetRole(RoleType.ChaosRepressor);
                                }
                                else if (tickets >= 10)
                                {
                                    ply.SetRole(RoleType.ChaosMarauder);
                                }
                                else
                                {
                                    ply.SetRole(RoleType.ChaosRifleman);
                                }
                                Respawn.GrantTickets(team, -1);
                                break;
                            }
                            tickets--;
                        }
                    }
                });
            }
        }
    }
}
