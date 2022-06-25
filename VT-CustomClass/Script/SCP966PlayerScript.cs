using Synapse;
using Synapse.Api.Enum;
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
    public class SCP966cript : AbstractRole, IScpDeathAnnonce
    {
        public string ScpName => "9 6 6";
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamManager.Group.SCPenemy.ToList();

        protected override List<int> FriendsList => TeamManager.Group.SCPally.ToList();

        protected override List<int> FfFriendsList => FriendsList;

        protected override RoleType RoleType => RoleType.Scp0492;

        protected override int RoleTeam => (int)TeamID.SCP;

        protected override int RoleId => (int)RoleID.SCP966;

        protected override string RoleName => Plugin.Instance.Config.Scp966Name;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.Scp966Config;

        protected override void InitEvent()
        {
            Server.Get.Events.Player.PlayerDamageEvent += OnAttack;
        }

        private static void OnAttack(PlayerDamageEventArgs ev)
        {
            if (ev.Allow && ev.Killer?.CustomRole is SCP966cript)
            {
                ev.Damage = 10;
                if (!ev.Victim.IsUTR())
                {
                    ev.Victim.GiveEffect(Effect.Concussed, 1, 20);
                    ev.Victim.GiveEffect(Effect.Deafened, 1, 20);
                    ev.Victim.GiveEffect(Effect.Exhausted, 1, 20);
                    ev.Victim.GiveEffect(Effect.Asphyxiated, 1, 5);
                }
            }
        }
    }
}
