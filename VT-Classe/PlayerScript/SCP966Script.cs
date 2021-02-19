using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class SCP966cript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.SCP966Config;

        public override MoreClasseID ClasseID => MoreClasseID.SCP966;

        public override Team ClasseTeam => Team.SCP;

        public override RoleType ClasseRole => RoleType.Scp0492;
        public override void AdditionalInit()
        {
        }
    }
}
