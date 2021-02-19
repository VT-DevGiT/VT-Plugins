using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class SCP682Script : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.SCP682Config;

        public override MoreClasseID ClasseID => MoreClasseID.SCP682;

        public override Team ClasseTeam => Team.SCP;

        public override RoleType ClasseRole => RoleType.Scp93953;
        public override void AdditionalInit()
        {
        }
    }
}
