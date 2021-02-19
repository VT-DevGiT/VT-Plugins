using CustomClass.Config;

namespace CustomClass.PlayerScript
{
    public class NTFVirologueScript : BasePlayerScript
    {
        public override IBaseConfig Config => MoreClasseClass.VirologueConfig;

        public override MoreClasseID ClasseID => MoreClasseID.NTFVirologue;

        public override Team ClasseTeam => Team.MTF;

        public override RoleType ClasseRole => RoleType.NtfScientist;
        public override void AdditionalInit()
        {
        }
    }
}
