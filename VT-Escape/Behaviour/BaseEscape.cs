using Synapse.Api;
using System.Linq;
using UnityEngine;
using VT_Api.Core.Behaviour;
using VT_Api.Core.Enum;

namespace VTEscape
{
    [API]
    public abstract class BaseEscape : RepeatingBehaviour
    {
        public BaseEscape()
        {
            this.RefreshTime = 100;
        }
        protected override void Start()
        {
            Player = gameObject.GetPlayer();
            this.enabled = Player.RoleType != RoleType.Spectator;
            base.Start();
        }

        public Player Player { get; private set; }
        public abstract Vector3 Postion { get; }
        public abstract int Radius { get; }
        public abstract EscapeType EscapeType { get; }
        

        public SerializedEscapeConfig GetConfig()
        {
            // Get for the roll
            SerializedEscapeConfig configEscape = Plugin.Instance.Config.EscapeList.FirstOrDefault(c => Player.RoleID == c.RoleID && EscapeType == c.Escape && 
                ((Player.Cuffer == null && c.CufferTeamID == (int)TeamID.None) ||  Player.Cuffer?.TeamID == c.CufferTeamID));
            if (configEscape != null)
            {
                return configEscape;
            }

            // Get for the team
            configEscape = Plugin.Instance.Config.EscapeList.FirstOrDefault(c => Player.TeamID == c.TeamID && EscapeType == c.Escape &&
                ((Player.Cuffer == null && c.CufferTeamID == (int)TeamID.None) || Player.Cuffer?.TeamID == c.CufferTeamID));
            if (configEscape != null)
            {
                return configEscape;
            }
            return null;
        }

        protected override void BehaviourAction()
        {
            if (Player.RoleType == RoleType.Spectator)
                enabled = false;
            if (Vector3.Distance(base.transform.position, Postion) < Radius)
            {
                var config = GetConfig();

                if (config != null)
                {
                    Plugin.Instance.CallCustomEscapeEvent(Player, Player.Cuffer, EscapeType, config.EscapeMessage, config.StartWarHead, Player.RoleID, config.NewRoleID);
                    config.Use(Player);
                }

                return;
            }
        }
    }
}

