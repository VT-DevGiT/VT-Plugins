using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class TestClassScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.TestClassConfig;

        public override MoreClasseID ClasseID => MoreClasseID.TestClass;

        public override Team ClasseTeam => Team.TUT;

        public override RoleType ClasseRole => RoleType.Tutorial;
        public override void AdditionalInit()
        {
        }
    }
}
