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
            Refresh();
            base.Start();
        }

        public Player Player { get; private set; }
        public abstract Vector3 Postion { get; }
        public abstract int Radius { get; }
        public abstract EscapeType EscapeType { get; }
        public SerializedEscapeConfig EscapeConfig { get; protected set; }


        public void Refresh()
        {
            this.enabled = Player.RoleType != RoleType.Spectator;

            if (Player.RoleType == RoleType.Spectator)
            {
                EscapeConfig = null;
                return;
            }
            
            // Get for the roll
            var configEscape = Plugin.Config.EscapeList.FirstOrDefault(c => Player.RoleID == c.RoleID && EscapeType == c.Escape && 
                ((Player.Cuffer == null && c.CufferTeamID == (int)TeamID.None) ||  Player.Cuffer?.TeamID == c.CufferTeamID));
            if (configEscape != null)
            {
                EscapeConfig = configEscape;
                return;
            }

            // Get for the team
            configEscape = Plugin.Config.EscapeList.FirstOrDefault(c => Player.TeamID == c.TeamID && EscapeType == c.Escape &&
                ((Player.Cuffer == null && c.CufferTeamID == (int)TeamID.None) || Player.Cuffer?.TeamID == c.CufferTeamID));
            if (configEscape != null)
            {
                EscapeConfig = configEscape;
                return;
            }
        }

        protected override void BehaviourAction()
        {
            if (Vector3.Distance(base.transform.position, Postion) < Radius)
            {
                if (EscapeConfig != null)
                    EscapeConfig.Use(Player);

                if (Player.CustomRole != null)
                    Player.TriggerEscape();

                return;
            }
        }
    }
}

