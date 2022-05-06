namespace BetterRespawn.Handlers
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using Respawning;
    using MEC;

    class Server
    {
        int first_cutoff = BetterRespawn.Instance.Config.firstCutoff;
        int second_cutoff = BetterRespawn.Instance.Config.secondCutoff;

        public void OnWaitingForPlayers()
        {
            Log.Info(message: "Loaded and waiting for players...");
        }

        public void RespawnTicketChecker(RespawningTeamEventArgs ev)
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
                        if (tickets > 0 && ply.IsDead == true && !ply.IsOverwatchEnabled)
                        {
                            if (BetterRespawn.Instance.Config.debug) { Log.Debug($"Spawning {ply.Nickname} at {tickets} tickets"); }
                            if (tickets >= first_cutoff)
                            {
                                ply.SetRole(RoleType.NtfCaptain);
                            }
                            else if (tickets >= second_cutoff)
                            {
                                ply.SetRole(RoleType.NtfSergeant);
                            }
                            else
                            {
                                ply.SetRole(RoleType.NtfPrivate);
                            }
                            Respawn.GrantTickets(team, -1);
                            tickets--;
                        }
                    }
                }
                else if (team == SpawnableTeamType.ChaosInsurgency)
                {
                    if (BetterRespawn.Instance.Config.debug) { Log.Debug("Spawning chaos..."); }
                    uint tickets = Respawn.ChaosTickets;
                    foreach (Player ply in Player.List)
                    {
                        if (tickets > 0 && ply.IsDead == true && !ply.IsOverwatchEnabled)
                        {
                            if (BetterRespawn.Instance.Config.debug) { Log.Debug($"Spawning {ply.Nickname} at {tickets} tickets"); }
                            if (tickets >= first_cutoff)
                            {
                                ply.SetRole(RoleType.ChaosRepressor);
                            }
                            else if (tickets >= second_cutoff)
                            {
                                ply.SetRole(RoleType.ChaosMarauder);
                            }
                            else
                            {
                                ply.SetRole(RoleType.ChaosRifleman);
                            }
                            Respawn.GrantTickets(team, -1);
                            tickets--;
                        }
                    }
                }
            });
        }
    }
}
