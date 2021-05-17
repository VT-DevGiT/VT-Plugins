using Grenades;
using MEC;
using Synapse;
using Synapse.Api.Enum;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using UnityEngine;
using VT_Referance.Method;
using VT_Referance.Variable;

namespace VThandcuff
{
    internal class EventHandlers
    {
        
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerCuffTargetEvent += OnCuff;
            Server.Get.Events.Player.PlayerUncuffTargetEvent += OnUnCuff;
            Server.Get.Events.Round.RoundRestartEvent += OnRestart;
        }

        private void OnRestart()
        {
            Plugin.Instance.CuffedPlayer.Clear();
        }

        private void OnUnCuff(PlayerUnCuffTargetEventArgs ev)
        {
            if (Plugin.Config.CuffLock)
            {
                if (ev.Player.ItemInHand.ID == (int)ItemType.Disarmer)
                { 
                    ev.Allow = true;
                    if (Plugin.Instance.CuffedPlayer.Contains(ev.Cuffed))
                        Plugin.Instance.CuffedPlayer.Remove(ev.Cuffed);
                }
                else
                    ev.Allow = false;
            }
        }

        private void OnCuff(PlayerCuffTargetEventArgs ev)
        {
            if (Plugin.Config.NCuffUTR && ev.Target.IsUTR())
            { 
                ev.Allow = false;
                return;
            }
            if (Plugin.Config.Cuff049 && ev.Target.RoleID == (int)RoleID.Scp049)
                ev.Allow = true;
            if (Plugin.Config.CuffAlly && ev.Target.RealTeam == ev.Cuffer.RealTeam)
                ev.Allow = true;
            if (Plugin.Config.CuffTuto && ev.Target.RoleType == RoleType.Tutorial)
                ev.Allow = true;
            if (Plugin.Config.CuffLock)
            {
                if (Math.Abs(ev.Cuffer.Rotation.y - ev.Target.Rotation.y) <= Plugin.Config.Angle)
                    ev.Allow = true;
                else
                    ev.Allow = false;
                if (ev.Allow == true)
                {
                    Plugin.Instance.CuffedPlayer.Add(ev.Target);
                    ev.Target.Cuffer = ev.Target;
                }
                ev.Allow = false;
            }
        }
    }
}