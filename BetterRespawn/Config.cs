namespace BetterRespawn
{
    using Exiled.API.Interfaces;
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool debug { get; set; } = false;
        public bool BalanceSCPSpawnrate { get; set; } = false;
    }
}
