using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class RoboticTacticalUnityScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.UTRConfig;

        public override MoreClasseID ClasseID => MoreClasseID.RoboticTacticalUnity;

        public override Team ClasseTeam => Team.MTF;

        public override RoleType ClasseRole => RoleType.NtfLieutenant;

        public override void AdditionalInit()
        {
        }
    }
}
