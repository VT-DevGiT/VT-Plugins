using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class NTFInfirmierScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.InfirmierConfig;

        public override MoreClasseID ClasseID => MoreClasseID.NTFInfirmier;

        public override Team ClasseTeam => Team.MTF;

        public override RoleType ClasseRole => RoleType.NtfLieutenant;
        public override void AdditionalInit()
        {
        }
    }
}
