using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class DirecteurSiteScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.ConciergeConfig;

        public override MoreClasseID ClasseID => MoreClasseID.DirecteurSite;

        public override Team ClasseTeam => Team.RSC;

        public override RoleType ClasseRole => RoleType.Scientist;
        public override void AdditionalInit()
        {
        }
    }
}
