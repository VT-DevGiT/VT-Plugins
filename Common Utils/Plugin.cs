using Synapse.Api.Plugin;
using VT_Api.Core.Plugin;
using HarmonyLib;
using Synapse;


namespace Common_Utiles
{
    /*
     * 
     *  *****  *****
     * **************
     *  ************
     *   *********
     *     *****
     *       * 
     */
    [PluginInformation(
        Name = "Common Utiles",
        Author = "Oka, update by Warkis",
        Description = "add new functionality and config",
        LoadPriority = 5,
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.5.0"
        )]
    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config.Config>
    {
        public override bool AutoRegister => false;        
    }
}