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
        [Description("When the tickets are higher than this number, Commander/chaos lmg spawn")]
        public int firstCutoff { get; set; } = 15;
        [Description("When the tickets are higher than this number, lieutenant/chaos shotgun spawn")]
        public int secondCutoff { get; set; } = 10;
    }
}
