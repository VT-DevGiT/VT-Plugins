using Synapse;
using Synapse.Api.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VT_ModedClientAllow
{
    [PluginInformation(
    Name = "VT-ModedClient",
    Author = "VT",
    Description = "Allow Moded Client (but create fail in ban systeme)",
    LoadPriority = 0,
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.0.0.1"
    )]
    public class Plugin : AbstractPlugin
    {
        public override void Load()
        {
            var instance = new Harmony("VTModedClient");
            instance.PatchAll();
            Server.Get.Logger.Info("Moded Client Harmony Patch done!");


            base.Load();
        }

    }
}
