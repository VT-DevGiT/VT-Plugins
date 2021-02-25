using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class SPYScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.SPYConfig;

        public override MoreClasseID ClasseID => MoreClasseID.ICSpy;

        public override Team ClasseTeam => Team.CHI;

        public override RoleType ClasseRole => RoleType.ChaosInsurgency;
        public override void AdditionalInit()
        {
        }
    }
}
