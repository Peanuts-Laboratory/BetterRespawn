namespace BetterRespawn.Handlers
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using Respawning;
    using MEC;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    class Server
    {
        private List<Player> GetSCPList()
        {
            List<Player> scp_list = new List<Player>();

            if (BetterRespawn.Instance.Config.debug) { Log.Info("Generating scp_list"); }

            foreach (var ply in Player.List)
            {
                //if (BetterRespawn.Instance.Config.debug) { Log.Info($"{ply.Nickname} is scp: {ply.IsScp}"); }

                if (ply.IsScp)
                {
                    if (ply.Role == RoleType.Scp079)
                    {
                        if (BetterRespawn.Instance.Config.debug) { Log.Info($">>> {ply.Nickname} is computer, ignoring"); }
                    }
                    else
                    {
                        if (BetterRespawn.Instance.Config.debug) { Log.Info($">>> Adding {ply.Nickname} to scp_list"); }

                        scp_list.Add(ply);
                    }
                }
            }
            return scp_list;
        }


        private List<Player> SwapSCPToScientist(List<Player> scp_list, int correct_number_scps)
        {
            List<Player> new_scientist_list = new List<Player>();
            // remove random scps until we have the correct amount of scps
            while (scp_list.Count() > correct_number_scps-1)
            {
                int index = UnityEngine.Random.Range(0, scp_list.Count()-1);

                if (BetterRespawn.Instance.Config.debug) { Log.Info($"Removing {scp_list[index].Nickname} as role {scp_list[index].Role} at index {index}"); }

                scp_list[index].SetRole(RoleType.Scientist);
                new_scientist_list.Add(scp_list[index]);
                scp_list.RemoveAt(index);
            }
            return new_scientist_list;
        }


        private void FixHealth(List<Player> new_scientist_list)
        {
            foreach (var ply in new_scientist_list)
            {
                Timing.CallDelayed(0.5f, () =>
                {
                    if (BetterRespawn.Instance.Config.debug) { Log.Info($"Setting {ply.Nickname} hp to 100"); }

                    ply.MaxHealth = 100;
                    ply.Health = 100;
                });
            }
        }


        public void OnWaitingForPlayers()
        {
            Log.Info(message: "Loaded and waiting for players...");
        }


        public void BalanceSCPSpawnrate()
        {
            if (BetterRespawn.Instance.Config.debug) { Log.Info($"SCPSpawnrate config is set to: {BetterRespawn.Instance.Config.BalanceSCPSpawnrate}"); }

            if (BetterRespawn.Instance.Config.BalanceSCPSpawnrate == true) 
            {
                int number_of_players = Player.List.Count();
                int correct_number_scps = (int)Math.Ceiling((double)number_of_players / 10);

                if (BetterRespawn.Instance.Config.debug) { Log.Info($"Calculating variables..."); }

                if (BetterRespawn.Instance.Config.debug) { Log.Info($"number_of_players: {number_of_players} | correct_number_scps {correct_number_scps}"); }

                if (number_of_players > 10)
                {
                    Timing.CallDelayed(0.07f, () =>
                    {
                        List<Player> scp_list = GetSCPList();
                        List<Player> new_scientist_list = SwapSCPToScientist(scp_list, correct_number_scps);

                        Timing.CallDelayed(2f, () =>
                        {
                            FixHealth(new_scientist_list);
                        });
                    });
                }
                
            }
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
                        if (BetterRespawn.Instance.Config.debug) { Log.Info("Spawning ntf..."); }
                        int tickets = Respawn.NtfTickets;
                        while (tickets > 0)
                        {
                            foreach (var ply in Player.List)
                            {
                                if (ply.Team == Team.RIP && !ply.IsOverwatchEnabled)
                                {
                                    if (BetterRespawn.Instance.Config.debug) { Log.Info($"Spawning {ply.Nickname} at {tickets} tickets"); }
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
                            }
                            tickets--;
                        }
                    }
                });
            }
        }
    }
}
