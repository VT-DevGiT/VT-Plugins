using HarmonyLib;
using Mirror;
using Synapse;
using Synapse.Api.Plugin;
using System.Collections.Generic;
using UnityEngine;

namespace VT_IpLocker
{
    [PluginInformation(
        Author = "VT",
        Description = "Prevent foreigners from connecting to the server to avoid mass freekill",
        LoadPriority = 1,
        Name = "VT-IpLocker",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.1.1"
        )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-IpLocker")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
