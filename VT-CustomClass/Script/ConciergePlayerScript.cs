using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Api.Config;
using VT_Api.Core.Enum;
using VT_Api.Core.Roles;
using VT_Api.Core.Teams;
using VTCustomClass.Pouvoir;

namespace VTCustomClass.PlayerScript
{
    public class JanitorScript : AbstractRole
    {
        protected override string SpawnMessage => Plugin.Instance.Translation.ActiveTranslation.SpawnMessage;
            
        protected override List<int> EnemysList => new List<int> { (int)TeamID.SCP };

        protected override List<int> FriendsList => TeamManager.Group.MTFally.ToList();

        protected override RoleType RoleType => RoleType.ClassD;

        protected override int RoleTeam => (int)TeamID.RSC;

        protected override int RoleId => (int)RoleID.Janitor;

        protected override string RoleName => Plugin.Instance.Config.JanirtorName;

        protected override SerializedPlayerRole Config => Plugin.Instance.Config.JanirtorConfig;


        public override bool CallPower(byte power, out string message)
        {
            if (power == (int)PowerType.Clear)
            {
                List<Collider> collidersDoll = Physics.OverlapSphere(Player.Position, 2.5f).Where(e => e.gameObject.GetComponentInParent<Ragdoll>() != null).ToList();
                if (collidersDoll.Count != 0)
                {
                    Ragdoll doll = collidersDoll[0].gameObject.GetComponentInParent<Ragdoll>();
                    if (doll != null)
                        UnityEngine.Object.DestroyImmediate(doll.gameObject, true);
                }
                message = "Hard work, you clear the floor";
                return true;
            }
            message = "You ave only one power";
            return false;
        }
    }
}
