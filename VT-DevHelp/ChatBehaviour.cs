using Assets._Scripts.Dissonance;
using Mirror;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;

namespace VT079
{
    public class ChatBehaviour : NetworkBehaviour
    {

        Player player;
        public bool IAA { get; set; }
        public bool DOG { get; set; }
        public bool SCP { get; set; }
        public bool RAD { get; set; }
        public bool RIP { get; set; }

        private void Awake()
        {
            player = gameObject.GetPlayer();
        }
        void Start()
        {
            Server.Get.Events.Player.PlayerSpeakEvent += OnSpeck;
        }
        private void OnSpeck(PlayerSpeakEventArgs ev)
        {
            DissonanceUserSetup dissonanceUserSetup = player.gameObject.GetComponent<DissonanceUserSetup>();

            if (SCP)
                dissonanceUserSetup.SCPChat = true;
            if (RIP)
                dissonanceUserSetup.SpectatorChat = true;
            if (RAD)
                dissonanceUserSetup.RadioAsHuman = true;
            if (IAA)
                dissonanceUserSetup.SpeakerAs079 = true;
            if (DOG)
                dissonanceUserSetup.MimicAs939 = true;
        }


        void Update()
        {

        }
    }
}
