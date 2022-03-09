using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using UnityEngine;
using VT_Api.Core.Plugin;

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
    public class Plugin : VtAbstractPlugin<Plugin, EventHandler, Config>
    {
        public override bool AutoRegister => false;

        public Color firstColor;
        public Color secondColor;
        public Color thirdColor;

        public override void Load()
        {
            base.Load();
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
            thirdColor = new Color(Config.ThirdColor[0] / byte.MaxValue, Config.ThirdColor[1] / byte.MaxValue, Config.ThirdColor[2] / byte.MaxValue);
        }
    }
}
