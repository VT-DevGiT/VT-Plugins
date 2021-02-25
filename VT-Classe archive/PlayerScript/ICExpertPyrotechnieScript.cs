using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class ICExpertPyrotechnieScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.ExpertPyrotechnieICConfig;

        public override MoreClasseID ClasseID => MoreClasseID.ICExpertPyrotechnie;

        public override Team ClasseTeam => Team.CHI;

        public override RoleType ClasseRole => RoleType.ChaosInsurgency;
        public override void AdditionalInit()
        {
        }
    }
}
