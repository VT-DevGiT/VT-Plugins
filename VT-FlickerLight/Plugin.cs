using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using UnityEngine;

namespace VT_FlickerLight
{
    [PluginInformation(
        Name = "VT-FlickerLight",
        Author = "Antoniofo",
        Description = "Flicker the light at the start of the game",
        LoadPriority = 0,
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.0.1"
        )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-FlickerLight")]
        public static Config Config { get; set; }

        public Color firstColor;
        public Color secondColor;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandler();
            SetColorValue();
        }

        public override void ReloadConfigs()
        {
            SetColorValue();
            base.ReloadConfigs();
        }

        public void SetColorValue()
        {
            firstColor = new Color(Config.FirstColor[0] / byte.MaxValue, Config.FirstColor[1] / byte.MaxValue, Config.FirstColor[2] / byte.MaxValue);
            secondColor = new Color(Config.SecondColor[0] / byte.MaxValue, Config.SecondColor[1] / byte.MaxValue, Config.SecondColor[2] / byte.MaxValue);
        }
    }
}
