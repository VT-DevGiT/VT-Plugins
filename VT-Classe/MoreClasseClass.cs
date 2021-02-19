using CustomClass.Config;
using CustomClass.PlayerScript;
using Synapse;
using Synapse.Api.Plugin;
using System.Collections.Generic;

namespace CustomClass
{
    [PluginInformation(
         Author = "VT",
         Description = "More Classe for SCP:SL",
         LoadPriority = 1,
         Name = "MoreClasse",
         SynapseMajor = 2,
         SynapseMinor = 4,
         SynapsePatch = 1,
         Version = "v.1.1.1"
         )]
    public class MoreClasseClass : AbstractPlugin
    {
        public static MoreClasseClass Instance { get; private set; }

        #region Config
        [Synapse.Api.Plugin.Config(section = "Commands")]
        public static CommandsConfig CommandsConfig;

        [Synapse.Api.Plugin.Config(section = "Concierge")]
        public static ConciergeConfig ConciergeConfig;

        [Synapse.Api.Plugin.Config(section = "Expert Pyrotechnie FIM")]
        public static NTFExpertPyrotechnieConfig ExpertPyrotechnieFIMConfig;

        [Synapse.Api.Plugin.Config(section = "Expert Pyrotechnie IC")]
        public static ICExpertPyrotechnieConfig ExpertPyrotechnieICConfig;

        [Synapse.Api.Plugin.Config(section = "Expert Reconfinement")]
        public static NTFExpertReconfinementConfig ExpertReconfinementConfig;

        [Synapse.Api.Plugin.Config(section = "Garde Superviseur")]
        public static GardeSuperviseurConfig GardeSuperviseurConfig;

        [Synapse.Api.Plugin.Config(section = "Hacker")]
        public static ICHackerConfig HackerConfig;

        [Synapse.Api.Plugin.Config(section = "Infirmier")]
        public static NTFInfirmierConfig InfirmierConfig;

        [Synapse.Api.Plugin.Config(section = "Intrus")]
        public static ICIntrusConfig IntrusConfig;

        [Synapse.Api.Plugin.Config(section = "Kamikaze")]
        public static ICKamikazeConfig KamikazeConfig;

        [Synapse.Api.Plugin.Config(section = "Leader")]
        public static ICLeaderConfig LeaderConfig;

        [Synapse.Api.Plugin.Config(section = "Mastodonte")]
        public static ICMastodonteConfig MastodonteConfig;

        [Synapse.Api.Plugin.Config(section = "Scientifique Superviseur")]
        public static ScientifiqueSuperviseurConfig ScientifiqueSuperviseurConfig;

        [Synapse.Api.Plugin.Config(section = "Directeur du Site")]
        public static ScientifiqueSuperviseurConfig DirecteurSiteConfig;

        [Synapse.Api.Plugin.Config(section = "SCP008")]
        public static SCP008Config SCP008Config;

        [Synapse.Api.Plugin.Config(section = "SCP507")]
        public static SCP507Config SCP507Config;

        [Synapse.Api.Plugin.Config(section = "SCP682")]
        public static SCP682Config SCP682Config;

        [Synapse.Api.Plugin.Config(section = "SCP966")]
        public static SCP966Config SCP966Config;

        [Synapse.Api.Plugin.Config(section = "SCP999")]
        public static SCP999Config SCP999Config;

        [Synapse.Api.Plugin.Config(section = "SPY")]
        public static ICSpyConfig SPYConfig;

        [Synapse.Api.Plugin.Config(section = "Technicien")]
        public static TechnicienConfig TechnicienConfig;

        [Synapse.Api.Plugin.Config(section = "UTR")]
        public static RoboticTacticalUnityConfig UTRConfig;

        [Synapse.Api.Plugin.Config(section = "Virologue")]
        public static NTFVirologueConfig VirologueConfig;

        [Synapse.Api.Plugin.Config(section = "TestClass")]
        public static TestClassConfig TestClassConfig;
        #endregion

        #region Methode
        private void RegisterCustomRole()
        {
            var manager = Server.Get.RoleManager;
            manager.RegisterCustomRole<ConciergeScript>();
            manager.RegisterCustomRole<NTFExpertPyrotechnieScript>();
            manager.RegisterCustomRole<ICExpertPyrotechnieScript>();
            manager.RegisterCustomRole<GardeSuperviseurScript>();
            manager.RegisterCustomRole<ICHackerScript>();
            manager.RegisterCustomRole<NTFInfirmierScript>();
            manager.RegisterCustomRole<ICKamikazeScript>();
            manager.RegisterCustomRole<ICLeaderScript>();
            manager.RegisterCustomRole<ScientifiqueSuperviseurScript>();
            manager.RegisterCustomRole<DirecteurSiteScript>();
            manager.RegisterCustomRole<SCP008Script>();
            manager.RegisterCustomRole<SCP507Script>();
            manager.RegisterCustomRole<SCP682Script>();
            manager.RegisterCustomRole<SCP966cript>();
            manager.RegisterCustomRole<SCP999Script>();
            manager.RegisterCustomRole<SPYScript>();
            manager.RegisterCustomRole<TechnicienScript>();
            manager.RegisterCustomRole<ICIntrusScript>();
            manager.RegisterCustomRole<RoboticTacticalUnityScript>();
            manager.RegisterCustomRole<NTFVirologueScript>();
            manager.RegisterCustomRole<TestClassScript>();
        }

        public static List<ProbaInfo> GetRolePossible(int value)
        {
            List<ProbaInfo> result = new List<ProbaInfo>();
            if (value.IsScpID())
            {
                result.AddProba(MoreClasseID.SCP008, SCP008Config);
                result.AddProba(MoreClasseID.SCP682, SCP682Config);
                result.AddProba(MoreClasseID.SCP966, SCP966Config);
            }
            else if (value.IsMtfLieutenantID())
            {
                result.AddProba(MoreClasseID.NTFExpertPyrotechnie, ExpertPyrotechnieFIMConfig);
                result.AddProba(MoreClasseID.NTFExpertReconfinement, ExpertReconfinementConfig);
                result.AddProba(MoreClasseID.NTFInfirmier, InfirmierConfig);
                result.AddProba(MoreClasseID.NTFVirologue, VirologueConfig);
            }
            else if (value.IsMtfCadetID())
            {
                result.AddProba(MoreClasseID.ICSpy, SPYConfig);
            }
            else if (value.IsMtfGardID())
            {
                result.AddProba(MoreClasseID.GardeSuperviseur, GardeSuperviseurConfig);
                result.AddProba(MoreClasseID.Technicien, TechnicienConfig);
                result.AddProba(MoreClasseID.RoboticTacticalUnity, UTRConfig);
            }
            else if (value.IsChiID())
            {
                result.AddProba(MoreClasseID.ICLeader, LeaderConfig);
                result.AddProba(MoreClasseID.ICMastodonte, MastodonteConfig);
                result.AddProba(MoreClasseID.ICExpertPyrotechnie, ExpertPyrotechnieICConfig);
                result.AddProba(MoreClasseID.ICHacker, HackerConfig);
                result.AddProba(MoreClasseID.ICKamikaze, KamikazeConfig);
            }
            else if (value.IsCldID())
            {
                result.AddProba(MoreClasseID.Concierge, ConciergeConfig);
                result.AddProba(MoreClasseID.ICIntrus, IntrusConfig);
                result.AddProba(MoreClasseID.SCP507, SCP507Config);
                result.AddProba(MoreClasseID.SCP999, SCP999Config);
            }
            else if (value.IsRcsID())
            {
                result.AddProba(MoreClasseID.ScientifiqueSuperviseur, ScientifiqueSuperviseurConfig);
                result.AddProba(MoreClasseID.DirecteurSite, DirecteurSiteConfig);
            }
            else if (value.IsRipID())
            {

            }
            else if (value.IsTutID())
            {
                result.AddProba(MoreClasseID.TestClass, TestClassConfig);
            }
            result.CompletProba();
            return result;
        }
        #endregion

        public override void Load()
        {

            RegisterCustomRole();
            Instance = this;
            var trans = new Dictionary<string, string>
            {
                {"spawn","" },
            };
            Translation.CreateTranslations(trans);

            new EventHandlers();
        }
        internal static string GetTranslation(string key) => Instance.Translation.GetTranslation(key);
    }
}
