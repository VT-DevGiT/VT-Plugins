using HarmonyLib;
using Mirror;
using Synapse;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using UnityEngine;

namespace JournéeDeLaFondation
{
    [PluginInformation(
        Author = "VT, Bonjemus",
        Description = "Le plugin pour la journée de la fondation",
        LoadPriority = 5,
        Name = "JourneeDelaFondation",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.2.1"
        )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }
        public bool Actif = false;

        [Synapse.Api.Plugin.Config(section = "JourneeDelaFondation")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
