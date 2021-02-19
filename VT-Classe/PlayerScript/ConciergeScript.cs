using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class ConciergeScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.ConciergeConfig;

        public override MoreClasseID ClasseID => MoreClasseID.Concierge;

        public override Team ClasseTeam => Team.RSC;

        public override RoleType ClasseRole => RoleType.ClassD;
        public override void AdditionalInit()
        {

        }

    }
}
