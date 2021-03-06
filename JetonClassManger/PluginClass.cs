using Synapse.Api;
using Synapse.Api.Plugin;
using System;
using System.Collections.Generic;
using System.Text;

namespace JetonClassManger
{

    [PluginInformation(
        Name = "ClassJetonMangager",
        Author = "Silia",
        Description = "Azarus Plugin for Manage AzarusClassJeton",
        LoadPriority = 0,
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.0.0"
        )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "ClassJetonMangager")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
        }
        public bool PlayerCanSwitch ;


    }
    
}

