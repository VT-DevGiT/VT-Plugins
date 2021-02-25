using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class ICKamikazeScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.KamikazeConfig;

        public override MoreClasseID ClasseID => MoreClasseID.ICKamikaze;

        public override Team ClasseTeam => Team.CHI;

        public override RoleType ClasseRole => RoleType.ChaosInsurgency;
        public override void AdditionalInit()
        {
        }
    }
}
