using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class ScientifiqueSuperviseurScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.ScientifiqueSuperviseurConfig;

        public override MoreClasseID ClasseID => MoreClasseID.ScientifiqueSuperviseur;

        public override Team ClasseTeam => Team.RSC;

        public override RoleType ClasseRole => RoleType.Scientist;
        public override void AdditionalInit()
        {
        }
    }
}
