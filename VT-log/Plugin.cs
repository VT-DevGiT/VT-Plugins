using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using System;
using VT_Api.Core.Plugin;

namespace VTLog
{
    [PluginInformation(
    Name = "VT-Log",
    Author = "VT",
    Description = "Log all events",
    LoadPriority = 0,
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.0.0.1"
    )]

    public class Plugin : VtAbstractPlugin<EventHandlers>
    {
        public override bool AutoRegister => false;

        public override void Load()
        {
            Log.CreateNew();

            ServerConsole.ConsoleOutputs.Add(new Logger());//TODO FIX THIS
        }

        public override void Unload(object sender, EventArgs e)
        {
            Server.Get.Logger.Send("Shoot Down", ConsoleColor.Magenta);
            Log.Write("--------------- End of Log ---------------");
        }

        public class Logger : IOutput
        {
            public void Print(string text) => Log.Write(text);

        }
    }
}
