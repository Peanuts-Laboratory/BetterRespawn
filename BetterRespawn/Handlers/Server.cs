namespace BetterRespawn.Handlers
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using Respawning;
    using MEC;
    using System.Collections.Generic;

    class Server
    {
        public void OnWaitingForPlayers()
        {
            Log.Info(message: "Loaded and waiting for players...");
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
                        foreach (Player ply in Player.List)
                        {
                            if (tickets != 0 && ply.IsDead == true && !ply.IsOverwatchEnabled)
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
                        foreach (Player ply in Player.List)
                        {
                            if (tickets != 0 && ply.IsDead == true && !ply.IsOverwatchEnabled)
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
