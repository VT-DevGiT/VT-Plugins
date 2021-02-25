using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class SCP507Script : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.SCP507Config;

        public override MoreClasseID ClasseID => MoreClasseID.SCP507;

        public override Team ClasseTeam => Team.CHI;

        public override RoleType ClasseRole => RoleType.ClassD;
        public override void AdditionalInit()
        {
        }
    }
}
