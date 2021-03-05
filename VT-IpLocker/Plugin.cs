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
        Description = "Allows you to activate grenades remotely",
        LoadPriority = 1,
        Name = "VT-VTIpLocker",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.1.1"
        )]
    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        [Synapse.Api.Plugin.Config(section = "VT-VTIpLocker")]
        public static Config Config;

        public override void Load()
        {
            Instance = this;
            base.Load();
            new EventHandlers();
        }
    }
}
