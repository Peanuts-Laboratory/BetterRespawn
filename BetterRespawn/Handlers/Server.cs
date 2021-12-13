namespace BetterRespawn.Handlers
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using Respawning;
    using MEC;
    class Server
    {
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
                    if (BetterRespawn.Instance.Config.debug) { Log.Info("Spawning ntf..."); }
                    int tickets = Respawn.NtfTickets;
                    while (tickets > 0)
                    {

                        foreach (var ply in Player.List)
                        {
                            if (ply.Team == Team.RIP && !ply.IsOverwatchEnabled)
                            {
                                if (BetterRespawn.Instance.Config.debug) { Log.Info($"Spawning {ply.Nickname} at {tickets} tickets"); }
                                ply.SetRole(RoleType.NtfPrivate);
                                Respawn.GrantTickets(team, -1);
                                break;
                            }
                        }
                        tickets--;
                    }
                }
                else if (team == SpawnableTeamType.ChaosInsurgency)
                {
                    if (BetterRespawn.Instance.Config.debug) { Log.Info("Spawning chaos..."); }
                    int tickets = Respawn.ChaosTickets;
                    while (tickets > 0)
                    {
                        foreach (var ply in Player.List)
                        {
                            if (ply.Team == Team.RIP && !ply.IsOverwatchEnabled)
                            {
                                if (BetterRespawn.Instance.Config.debug) { Log.Info($"Spawning {ply.Nickname} at {tickets} tickets"); }
                                ply.SetRole(RoleType.ChaosRifleman);
                                Respawn.GrantTickets(team, -1);
                                break;
                            }
                        }
                        tickets--;
                    }
                }
            });
        }
    }
}
