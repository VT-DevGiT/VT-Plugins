using Synapse;
using Synapse.Api.Plugin;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VTData
{
    [PluginInformation(
Name = "VT-Data",
Author = "VT",
Description = "Le plugin pour les données pour le site de Старски",
LoadPriority = 1,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v0.1.1"
)]

    public class Plugin : AbstractPlugin
    {
        public static Plugin Instance { get; private set; }

        /// <summary>
        /// Steam ID player / DateTime
        /// </summary>
        public static Dictionary<string, DateTime> PlayerJoinDate;

        public override void Load()
        {
            new EventHandlers();
            Instance = this;
            base.Load();
        }
    }
}
