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
        [Description("When enabled, team respawns deplete all tickets instead of half")]
        public bool RespawnTeams { get; set; } = false;
    }
}
