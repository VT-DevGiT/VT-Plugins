using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class ICHackerScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.HackerConfig;

        public override MoreClasseID ClasseID => MoreClasseID.ICHacker;

        public override Team ClasseTeam => Team.CHI;

        public override RoleType ClasseRole => RoleType.ChaosInsurgency
            ;
        public override void AdditionalInit()
        {
        }
    }
}
