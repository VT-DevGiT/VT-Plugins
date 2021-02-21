using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public class RoboticTacticalUnityScript : BasePlayerScript
    {
        protected override List<int> EnemysList => new List<int> { (int)Team.CHI };

        protected override List<int> FriendsList => new List<int> { (int)Team.MTF, (int)Team.RSC };

        protected override RoleType RoleType => RoleType.NtfLieutenant;

        protected override int RoleTeam => (int)Team.MTF;

        protected override int RoleId => (int)MoreClasseID.UTR;

        protected override string RoleName => PluginClass.ConfigRoboticTaticalUnity.RoleName;

        protected override AbstractConfigSection Config => PluginClass.ConfigRoboticTaticalUnity;

        protected override void AditionalInit()
        {
            Player.GiveEffect(Effect.Visuals939);
            Player.Hub.playerStats.artificialHpDecay = 0;
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerItemUseEvent += OnUseIteam;
        }

        public override void DeSpawn()
        {
            base.DeSpawn();
            Server.Get.Events.Player.PlayerItemUseEvent -= OnUseIteam;
        }

        private void OnUseIteam(PlayerItemInteractEventArgs ev)
        {
            if (ev.Player == Player)
            {
                if (ev.CurrentItem.ItemCategory == ItemCategory.Medical)
                    ev.Allow = false;
                if (ev.CurrentItem.ItemCategory == ItemCategory.SCPItem)
                    ev.Allow = false;
            }
        }
    }
}
