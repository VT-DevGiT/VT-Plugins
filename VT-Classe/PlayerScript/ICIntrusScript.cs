using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class ICIntrusScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.IntrusConfig;

        public override MoreClasseID ClasseID => MoreClasseID.ICIntrus;

        public override Team ClasseTeam => Team.CHI;

        public override RoleType ClasseRole => RoleType.NtfCadet;
        public override void AdditionalInit()
        {
        }
    }
}
