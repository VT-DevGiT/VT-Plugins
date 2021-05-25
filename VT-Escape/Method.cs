using MEC;
using Synapse;
using Synapse.Api;
using System.Collections.Generic;
using VT_Referance.Method;

namespace VTEscape
{
    public class Method
    {
        public IEnumerator<float> WarHeadEscape(int waitforready = 3)
        {
            yield return Timing.WaitForSeconds(10f);
            while (waitforready > 0)
            {
                Methods.PlayAmbientSound(7);
                waitforready--;
                yield return Timing.WaitForSeconds(1f);
            }
            if (Plugin.Config.WarHeadLockEnabled)
            {
                Map.Get.Nuke.InsidePanel.Locked = true;
            }
            if (Round.Get.RoundIsActive)
                Server.Get.Map.Nuke.StartDetonation();
        }

        static public void ChangeRole(Player player, int Role)
        {
            player.Inventory.Clear();
            player.RoleID = Role;
        }
    }
}
