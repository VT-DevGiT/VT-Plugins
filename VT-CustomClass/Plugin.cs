using HarmonyLib;
using Synapse;
using Synapse.Api.Plugin;
using Synapse.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Core.Enum;
using VT_Api.Core.Plugin;
using VTCustomClass.CustomTeam;
using VTCustomClass.PlayerScript;

namespace VTCustomClass
{
    [PluginInformation(
        Author = "VT",
        Description = "A plugin for add new class",
        LoadPriority = 3,
        Name = "VT-CustomClass",
        SynapseMajor = SynapseController.SynapseMajor,
        SynapseMinor = SynapseController.SynapseMinor,
        SynapsePatch = SynapseController.SynapsePatch,
        Version = "v.1.3.2")]
    public class Plugin : VtAbstractPlugin<Plugin, EventHandlers, Config, Translation>
    {
        //TODO : Fix bug null error Expetion at the start of the round

        public override bool AutoRegister =>  true;

        //kill Enti-Cheat
        private void PatchAll()
        {
            var instance = new Harmony("VTCustomClass");
            instance.PatchAll();
            Server.Get.Logger.Info("Custom class Harmony Patch done!");
        }

        public override void Load()
        {
            base.Load();
            Translation.AddTranslation(new Translation {
                SpawnMessage = "<color=blue><b>Tu es à présent</b></color> <color=red><b>%RoleName%</b></color>\\n<b>Press Esc pour fermer</b>",
                VentMessage = "Vous pouvez rester encore %Time% secondes dans la ventilation",
                NoTimeVentMessage = "Vous vous trouvez dans les ventilation",
                PowerCooldown = "vous pouvez utiliser ce pouvoir dans %Time% secondes"
            }, "FRENCH");
            PatchAll();

            if (Server.Get.TeamManager.IsIDRegistered((int)TeamID.AND))
            {
                Server.Get.RoleManager.RegisterCustomRole<AndersonUTRlightScript>();
                Server.Get.RoleManager.RegisterCustomRole<AndersonUTRheavyScript>();
            }

            // Parse the config
            ParseConfig();
        }

        void ParseConfig()
        { 
            for (int i = 0; i < Config.SpawnClassConfigs.Count; i++)
                if (Config.SpawnClassConfigs[i].SpawnChance < 1)
                {
                    Config.SpawnClassConfigs.RemoveAt(i);
                    i--;
                }

            for (int i = 0; i < Config.SpawnReplaceScpClassConfig.Count; i++)
                if (Config.SpawnReplaceScpClassConfig[i].SpawnChance < 1)
                {
                    Config.SpawnReplaceScpClassConfig.RemoveAt(i);
                    i--;
                }

            for (int i = 0; i < Config.RespawnClassConfig.Count; i++)
                if (Config.RespawnClassConfig[i].SpawnChance < 1)
                {
                    Config.RespawnClassConfig.RemoveAt(i);
                    i--;
                }
        }

        public override void ReloadConfigs()
        {
            base.ReloadConfigs();
            ParseConfig();
        }

        [Obsolete("Use auto Register Systeme")]
        public void RegisterCustomTeam()
        {
            Server.Get.TeamManager.RegisterTeam<NetralSCPTeam>();
            Server.Get.TeamManager.RegisterTeam<BerserkSCPTeam>();
            Server.Get.TeamManager.RegisterTeam<VIPTeam>();
        }

        [Obsolete("Use auto Register Systeme")]
        public void RegisterCustomRole()
        {
            if (Server.Get.TeamManager.IsIDRegistered((int)TeamID.AND))
            { 
                Server.Get.RoleManager.RegisterCustomRole<AndersonUTRlightScript>();
                Server.Get.RoleManager.RegisterCustomRole<AndersonUTRheavyScript>();
            }
            Server.Get.RoleManager.RegisterCustomRole<JanitorScript>();
            Server.Get.RoleManager.RegisterCustomRole<DirecteurSiteScript>();
            Server.Get.RoleManager.RegisterCustomRole<ScientifiqueSuperviseurScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIExpertPyrotechnieScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIHackerScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIInfirmierScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIIntrusScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIKamikazeScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHILeaderScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHIMastodonteScript>();
            Server.Get.RoleManager.RegisterCustomRole<CHISPYScript>();
            Server.Get.RoleManager.RegisterCustomRole<GardeSuperviseurScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFLieutenantScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFCommanderScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFLieutenantColonel>();
            Server.Get.RoleManager.RegisterCustomRole<NTFExpertPyrotechnieScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFExpertReconfinementScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFInfirmierScript>();
            Server.Get.RoleManager.RegisterCustomRole<NTFVirologueScript>();
            Server.Get.RoleManager.RegisterCustomRole<FoundationUTRScript>();
            Server.Get.RoleManager.RegisterCustomRole<SCP008Script>();
            Server.Get.RoleManager.RegisterCustomRole<SCP1048cript>();
            Server.Get.RoleManager.RegisterCustomRole<SCP682Script>();
            Server.Get.RoleManager.RegisterCustomRole<SCP953Script>();
            Server.Get.RoleManager.RegisterCustomRole<SCP966cript>();
            Server.Get.RoleManager.RegisterCustomRole<SCP999Script>();
            Server.Get.RoleManager.RegisterCustomRole<StaffClassScript>();
            Server.Get.RoleManager.RegisterCustomRole<TechnicienScript>();
            Server.Get.RoleManager.RegisterCustomRole<TestClassScript>();
            Server.Get.RoleManager.RegisterCustomRole<ZoneManagerScript>();
            Server.Get.RoleManager.RegisterCustomRole<GardePrisonScript>();
            Server.Get.RoleManager.RegisterCustomRole<MTFUTRScript>();
        }

    }
}
