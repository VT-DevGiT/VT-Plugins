using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using System;

namespace VT939
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnSpawn;
            //Server.Get.Events.Player.PlayerSpeakEvent += OnSpeak;
        }

        private void OnSpeak(PlayerSpeakEventArgs ev)
        {
            Server.Get.Logger.Info($"OnSpeack {ev.Player?.NickName} - {ev.IntercomTalk} - {ev.RadioTalk} - {ev.Scp939Talk} - {ev.ScpChat} - {ev.SpectatorChat}");
        }

        private void OnSpawn(PlayerSetClassEventArgs ev)
        {
            if (ev.Role.Is939())
            {
                if (ev.Player.gameObject.TryGetComponent(out Scp939Controller customScp939))
                    customScp939.Destroy();

                ev.Player.gameObject.AddComponent<Scp939Controller>();
            }
        }


    }
}