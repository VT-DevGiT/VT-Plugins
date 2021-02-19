using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class SCP008Script : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.SCP008Config;

        public override MoreClasseID ClasseID => MoreClasseID.SCP008;

        public override Team ClasseTeam => Team.SCP;

        public override RoleType ClasseRole => RoleType.Scp0492;
        public override void AdditionalInit()
        {
        }
    }
}
