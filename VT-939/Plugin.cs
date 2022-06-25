using Synapse.Api.Plugin;
using VT_Api.Core.Plugin;

namespace VT939
{
    [PluginInformation(
Name = "VT-939",
Author = "VT",
Description = "Better939 for SynapseSl",
LoadPriority = 5,
SynapseMajor = SynapseController.SynapseMajor,
SynapseMinor = SynapseController.SynapseMinor,
SynapsePatch = SynapseController.SynapsePatch,
Version = "v.1.3.0"
)]

    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config, Translation>
    {
        public override bool AutoRegister => false;

        public override void Load()
        {
            base.Load();
            Translation.AddTranslation(new Translation()
            {
                SpawnMessage = "<size=20><color=#00FFFF>Vous êtes apparu en tant que version améliorée de <color=#FF0000>SCP-939</color>"
                                + "\nTu es plus rapide que les humains, ta <color=#FF0000>faim</color> augmentera après que tu ai subi des dégâts."
                                + "\nPlus de colère signifie plus de dégâts infligés aux humains."
                                + "\nAprès avoir <color=#FF0000>blessé</color> quelqu'un, vous serez ralenti pendant <color=#FF0000>{0}</color> secondes</color></size>"
            });
        }
    }
}
