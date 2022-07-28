using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VT_Api.Extension;

namespace VTCustomClass.PlayerScript
{
    public class NTFExpertReconfinementScript : AbstractRole
    {
        #region Properties & Variable
        protected override string SpawnMessage => Plugin.Instance.Translation.GetForPlayer(Player).SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.MTFenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.MTFally.ToList();

        protected override RoleType RoleType => RoleType.NtfSergeant;

        protected override int RoleTeam => (int)TeamID.NTF;

        protected override int RoleId => (int)RoleID.NtfExpertReconfinement;

        protected override string RoleName => Plugin.Instance.Config.ReconfinementExpName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.ReconfinementExpConfig;


        private bool _target096 = false;
        #endregion

        #region Events
        protected override void InitEvent()
        {
            Server.Get.Events.Scp.Scp096.Scp096AddTargetEvent += OnTarget;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
        }

        private static void OnDamage(PlayerDamageEventArgs ev)
        {
            if (ev.Killer?.CustomRole is NTFExpertReconfinementScript role && ev.Victim.RoleID == (int)RoleType.Scp096)
                role._target096 = true;
        }

        private static void OnTarget(Scp096AddTargetEventArgument ev)
        {
            if (ev.Player?.CustomRole is NTFExpertReconfinementScript role && !role._target096)
            {
                ev.Scp096.Scp096Controller.RemoveTarget(ev.Player);
                ev.Allow = false;
            }
        }
        #endregion
    }
}
