namespace BetterRespawn
{
    using System;
    using Exiled.API.Features;
    using Exiled.API.Enums;

    using Server = Exiled.Events.Handlers.Server;
    public class BetterRespawn : Plugin<Config>
    {
        private static readonly Lazy<BetterRespawn> LazyInstance = new Lazy<BetterRespawn>(valueFactory: () => new BetterRespawn());
        public static BetterRespawn Instance => LazyInstance.Value;
        public override PluginPriority Priority { get; } = PluginPriority.Higher;

        private Handlers.Server server;


        private BetterRespawn()
        {
        }


        public override void OnEnabled()
        {
            RegisterEvents();
            base.OnEnabled();
        }


        public override void OnDisabled()
        {
            UnregisterEvents();
            base.OnDisabled();
        }


        public void RegisterEvents()
        {
            server = new Handlers.Server();

            Server.WaitingForPlayers += server.OnWaitingForPlayers;
            Server.RespawningTeam += server.RespawnTicketChecker;
            Server.RoundStarted += server.BalanceSCPSpawnrate;
        }


        public void UnregisterEvents()
        {
            Server.WaitingForPlayers -= server.OnWaitingForPlayers;
            Server.RespawningTeam -= server.RespawnTicketChecker;
            Server.RoundStarted -= server.BalanceSCPSpawnrate;

            server = null;
        }
    }
}