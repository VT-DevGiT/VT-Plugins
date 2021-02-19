using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class ICMastodonteScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.MastodonteConfig;

        public override MoreClasseID ClasseID => MoreClasseID.ICMastodonte;

        public override Team ClasseTeam => Team.CHI;

        public override RoleType ClasseRole => RoleType.ChaosInsurgency;
        public override void AdditionalInit()
        {
        }
    }
}
