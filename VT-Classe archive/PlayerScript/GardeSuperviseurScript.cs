using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class GardeSuperviseurScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.GardeSuperviseurConfig;

        public override MoreClasseID ClasseID => MoreClasseID.GardeSuperviseur;

        public override Team ClasseTeam => Team.MTF;

        public override RoleType ClasseRole => RoleType.FacilityGuard;
        public override void AdditionalInit()
        {
        }
    }
}
