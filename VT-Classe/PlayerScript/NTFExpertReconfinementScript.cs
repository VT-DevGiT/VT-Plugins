using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class NTFExpertReconfinementScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.ExpertReconfinementConfig;

        public override MoreClasseID ClasseID => MoreClasseID.Concierge;

        public override Team ClasseTeam => Team.MTF;

        public override RoleType ClasseRole => RoleType.NtfLieutenant;
        public override void AdditionalInit()
        {
        }
    }
}
