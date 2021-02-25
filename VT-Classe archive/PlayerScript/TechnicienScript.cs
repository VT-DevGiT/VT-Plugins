using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class TechnicienScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.TechnicienConfig;

        public override MoreClasseID ClasseID => MoreClasseID.Technicien;

        public override Team ClasseTeam => Team.RSC;

        public override RoleType ClasseRole => RoleType.FacilityGuard;

        public override void AdditionalInit()
        {
        }
    }
}
