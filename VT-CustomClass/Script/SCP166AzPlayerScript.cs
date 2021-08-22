using VTCustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using VT_Referance.PlayerScript;
using VT_Referance.Variable;
using static VT_Referance.Variable.Data;

namespace VTCustomClass.PlayerScript
{
    public class SCP166AzScript : BasePlayerScript, IScpRole
    {
        public string ScpName => "1 6 6";
        protected override string SpawnMessage => Plugin.PluginTranslation.ActiveTranslation.SpawnMessage;

        protected override List<int> EnemysList => TeamGroupe.NetralSCPennemy;

        protected override List<int> FriendsList => TeamGroupe.SCPally;

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.NetralSCP;

        protected override int RoleId => (int)RoleID.SCP166Az;

        protected override string RoleName => Plugin.ConfigSCP166Az.RoleName;

        protected override AbstractConfigSection Config => Plugin.ConfigSCP166Az;

        protected override void AditionalInit()
        {
            ActiveComponent<Green>();
        }

        protected override void Event()
        {
            Server.Get.Events.Player.PlayerKeyPressEvent += OnKeyPress;
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPickUp;
            Server.Get.Events.Player.PlayerCuffTargetEvent += OnCuff;
        }

        private void OnCuff(PlayerCuffTargetEventArgs ev)
        {
            if (ev.Target == Player)
                ev.Allow = false;
        }

        private void OnPickUp(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Player == Player && ev.Item.ItemCategory != ItemCategory.Firearm && ev.Item.ItemCategory == ItemCategory.Radio)
                ev.Allow = false;
        }

        public override bool CallPower(int power)
        {
            if ((int)PowerType.BadGreen == power)
            {
                Player.GetComponent<Green>().DamagGreen = !Player.GetComponent<Green>().DamagGreen;
                return true;
            }
            else if ((int)PowerType.Attack == power)
            {
                var victim = Player.LookingAt.GetPlayer();
                if (victim != null)
                {
                    victim.Hurt(100);
                    return true;
                }
            }
            return false;
        }

        private void OnKeyPress(PlayerKeyPressEventArgs ev)
        {
            if (ev.Player == Player)
            {
                if (ev.KeyCode == UnityEngine.KeyCode.Alpha1)
                    CallPower((int)PowerType.BadGreen);
                if (ev.KeyCode == UnityEngine.KeyCode.Alpha1)
                    CallPower((int)PowerType.Attack);
            }
        }

        public override void DeSpawn()
        {
            KillComponent<Green>();
            base.DeSpawn();
            Server.Get.Events.Player.PlayerKeyPressEvent -= OnKeyPress;
            Server.Get.Events.Player.PlayerPickUpItemEvent -= OnPickUp;
            Server.Get.Events.Player.PlayerCuffTargetEvent -= OnCuff;
        }
    }
}
