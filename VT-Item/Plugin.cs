using HarmonyLib;
using Mirror;
using Synapse;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using UnityEngine;

namespace VT_Item
{
    [PluginInformation(
        Author = "VT",
        Description = "Add new Cool Item",
        LoadPriority = 100,
        Name = "VT-Item",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.1.1"
        )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-Item")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
