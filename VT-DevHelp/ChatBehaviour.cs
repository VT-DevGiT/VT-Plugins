using Assets._Scripts.Dissonance;
using Mirror;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;

namespace VTDevHelp
{
    public class ChatBehaviour : NetworkBehaviour
    {

        Player player;
        public int chat { get; set; }

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

            if (chat == 1)
            {
                dissonanceUserSetup.SCPChat = true;
                dissonanceUserSetup.SpectatorChat = false;
                dissonanceUserSetup.RadioAsHuman = false;
                dissonanceUserSetup.SpeakerAs079 = false;
                dissonanceUserSetup.MimicAs939 = false;
            }
            if (chat == 2)
            {
                dissonanceUserSetup.SCPChat = false;
                dissonanceUserSetup.SpectatorChat = true;
                dissonanceUserSetup.RadioAsHuman = false;
                dissonanceUserSetup.SpeakerAs079 = false;
                dissonanceUserSetup.MimicAs939 = false;
            }
            if (chat == 3)
            {
                dissonanceUserSetup.SCPChat = false;
                dissonanceUserSetup.SpectatorChat = false;
                dissonanceUserSetup.RadioAsHuman = true;
                dissonanceUserSetup.SpeakerAs079 = false;
                dissonanceUserSetup.MimicAs939 = false;
            }
            if (chat == 4)
            {
                dissonanceUserSetup.SCPChat = false;
                dissonanceUserSetup.SpectatorChat = false;
                dissonanceUserSetup.RadioAsHuman = false;
                dissonanceUserSetup.SpeakerAs079 = true;
                dissonanceUserSetup.MimicAs939 = false;
            }
            if (chat == 5)
            {
                dissonanceUserSetup.SCPChat = false;
                dissonanceUserSetup.SpectatorChat = false;
                dissonanceUserSetup.RadioAsHuman = false;
                dissonanceUserSetup.SpeakerAs079 = false;
                dissonanceUserSetup.MimicAs939 = true;
            }
        }


        void Update()
        {

        }
    }
}
