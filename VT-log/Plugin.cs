using Synapse;
using Synapse.Api;
using Synapse.Api.Plugin;
using System;
using System.IO;
using VT_Api.Core.Plugin;

namespace VTLog
{
    [PluginInformation(
    Name = "VT-Log",
    Author = "VT",
    Description = "Save console log",
    LoadPriority = 0,
    SynapseMajor = SynapseController.SynapseMajor,
    SynapseMinor = SynapseController.SynapseMinor,
    SynapsePatch = SynapseController.SynapsePatch,
    Version = "v.1.0.0"
    )]
    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers>
    {
        public override bool AutoRegister => false;

        public override void Load()
        {
            Log.CreateNew();

            ServerConsole.ConsoleOutputs.Add(new Logger());
        }

        public override void Unload()
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
