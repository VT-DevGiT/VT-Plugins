using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class NTFExpertPyrotechnieScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.ExpertPyrotechnieFIMConfig;

        public override MoreClasseID ClasseID => MoreClasseID.NTFExpertPyrotechnie;

        public override Team ClasseTeam => Team.MTF;

        public override RoleType ClasseRole => RoleType.NtfLieutenant;
        public override void AdditionalInit()
        {
        }
    }
}
