using Mirror;
using Synapse.Api;
using System.Linq;
using UnityEngine;

namespace VTEscape
{
    public class NTFEscape : NetworkBehaviour
    {
        private Player player;
        public bool Enabled = true;
        private float _timer;

        private void Awake()
        {
            player = gameObject.GetPlayer();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (Enabled && _timer > 1f)
            {
                if (Vector3.Distance(base.transform.position, base.GetComponent<Escape>().worldPosition) <= Escape.radius)//AdvencedEscape.Config.rayonSortie)
                {
                    var configEscape = Plugin.Config.EscapeList.FirstOrDefault(p => player.RoleID == (int)p.Role
                        && EscapeEnum.MTF == p.Escape && player.Cuffer == p.Handcuffed);
                    if (configEscape != null)
                    {
                        if (configEscape.StartWarHead == true)
                            Method.WarHeadEscape(3);
                        if (configEscape.EscapeMessage != null)
                            Map.Get.Cassie(configEscape.EscapeMessage, false);
                        player.Inventory.Clear();
                        player.RoleID = (int)configEscape.Role;
                        return;
                    }
                    configEscape = Plugin.Config.EscapeList.FirstOrDefault(p => player.TeamID == (int)p.Team
                        && EscapeEnum.MTF == p.Escape && player.Cuffer == p.Handcuffed);
                    if (configEscape != null)
                    {
                        if (configEscape.StartWarHead == true)
                            Method.WarHeadEscape(3);
                        if (configEscape.EscapeMessage != null)
                            Map.Get.Cassie(configEscape.EscapeMessage, false);
                        player.Inventory.Clear();
                        player.RoleID = (int)configEscape.Role;
                        return;
                    }
                    player.Inventory.Clear();
                    player.RoleID = (int)RoleType.Spectator;
                }
            }

            if (_timer > 1f)
                _timer = 0f;
        }

        public void Destroy()
        {
            Enabled = false;
            DestroyImmediate(this, true);
        }
    }
}
