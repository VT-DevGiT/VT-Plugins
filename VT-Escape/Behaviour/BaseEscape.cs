using MEC;
using Synapse;
using Synapse.Api;
using System.Linq;
using UnityEngine;
using VT_Referance.Behaviour;
using VT_Referance.Variable;

namespace VTEscape
{
    [API]
    public abstract class BaseEscape : BaseRepeatingBehaviour
    {

        public BaseEscape()
        {
            this.RefreshTime = 100;
        }
        protected override void Start()
        {
            Player = gameObject.GetPlayer();
            ChangeClassEvent();
            base.Start();
        }

        public Player Player { get; private set; }
        public abstract Vector3 Postion { get; }
        public abstract int Radius { get; }
        public abstract EscapeType EscapeType { get; }
        public SerializedEscapeConfig EscapeConfig { get; }


        public SerializedEscapeConfig ChangeClassEvent()
        {
            this.enabled = Player.RoleType != RoleType.Spectator;

            if (Player.RoleType == RoleType.Spectator)
                return null;
            // Get for the roll
            var configEscape = Plugin.Config.EscapeList.FirstOrDefault(c => Player.RoleID == (int)c.Role && EscapeType == c.Escape && 
                ((Player.Cuffer == null && c.CufferTeam == TeamID.None) ||  Player.Cuffer?.TeamID == (int)c.CufferTeam));
            if (configEscape != null)
            {
                configEscape.Use(Player);
                return configEscape;
            }
            // Get for the team
            configEscape = Plugin.Config.EscapeList.FirstOrDefault(c => Player.TeamID == (int)c.Team && EscapeType == c.Escape &&
                ((Player.Cuffer == null && c.CufferTeam == TeamID.None) || Player.Cuffer?.TeamID == (int)c.CufferTeam));
            if (configEscape != null)
            {
                configEscape.Use(Player);
                return configEscape;
            }
            return null;
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

