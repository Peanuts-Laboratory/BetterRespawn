namespace BetterRespawn
{
    using Exiled.API.Interfaces;
    using System.ComponentModel;

    public sealed class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;
        [Description("Debug output for the console")]
        public bool debug { get; set; } = false;
        [Description("DEPRICATED [use config_gameplay.txt] Enabling this will account for the new updates lack of config (makes teams spawn like they used to, all tickets at once)")]
        public bool RespawnTeams { get; set; } = false;
        [Description("Enabling this will make scp's spawn like they did in older versions")]
        public bool BalanceSCPSpawnrate { get; set; } = false;
    }
}
