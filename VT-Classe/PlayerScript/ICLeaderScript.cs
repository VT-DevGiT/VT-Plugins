using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class ICLeaderScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.LeaderConfig;

        public override MoreClasseID ClasseID => MoreClasseID.ICLeader;

        public override Team ClasseTeam => Team.CHI;

        public override RoleType ClasseRole => RoleType.ChaosInsurgency;
        public override void AdditionalInit()
        {
        }
    }
}
