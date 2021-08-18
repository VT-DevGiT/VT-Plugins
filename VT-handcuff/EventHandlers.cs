using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using VT_Referance.Method;

namespace VThandcuff
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerCuffTargetEvent += OnCuff;
            Server.Get.Events.Player.PlayerUncuffTargetEvent += OnUnCuff;
        }

        private void OnUnCuff(PlayerUnCuffTargetEventArgs ev)
        {
            if (Plugin.Config.CuffLock && (ev.FreeWithDisarmer == false || ev.Player?.ItemInHand?.ID != (int)ItemType.Disarmer))
                ev.Allow = false;
        }

        private void OnCuff(PlayerCuffTargetEventArgs ev)
        {
            if (Plugin.Config.CuffId.Contains(ev.Target.RoleID))
                ev.Allow = true;
            else if (Plugin.Config.CuffAlly && ev.Target.RealTeam == ev.Cuffer.RealTeam)
                ev.Allow = true;

            if (Plugin.Config.Angle != 0 && Math.Abs(ev.Cuffer.Rotation.y - ev.Target.Rotation.y) > Plugin.Config.Angle)
                ev.Allow = false;
            else if (Plugin.Config.NCuffUTR && ev.Target.IsUTR())
                ev.Allow = false;

            if (Plugin.Config.CuffLock && ev.Allow == true)
            {
                ev.Target.Cuffer = ev.Target;
                ev.Target.Inventory.DropAll();
                ev.Allow = false;
            }
        }
    }
}