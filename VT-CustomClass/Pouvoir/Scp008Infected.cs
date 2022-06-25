using Synapse.Api;
using Synapse.Api.Enum;
using VT_Api.Core.Behaviour;
using VT_Api.Core.Enum;
using VT_Api.Extension;

namespace VTCustomClass.Pouvoir
{
    public class Scp008Infected : RoundBehaviour
    {
        public Player Player { get; private set; }
        public Player Scp008 { get; set; }


        void Start()
        {
            Player = gameObject.GetPlayer();
        }

        void OnEnable()
        {
            if (!Player.IsUTR() || Player.RoleID == (int)RoleID.NtfVirologue)
                Player.GiveEffect(Effect.Poisoned, 2, -1);
        }
    }
}
