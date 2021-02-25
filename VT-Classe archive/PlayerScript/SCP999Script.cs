using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class SCP999Script : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.SCP999Config;

        public override MoreClasseID ClasseID => MoreClasseID.SCP999;

        public override Team ClasseTeam => Team.TUT;

        public override RoleType ClasseRole => RoleType.ClassD;
        public override void AdditionalInit()
        {
        }
    }
}
