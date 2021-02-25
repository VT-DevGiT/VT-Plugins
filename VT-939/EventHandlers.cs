using Dissonance;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;

namespace VT939
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnSpawn;
            Server.Get.Events.Player.PlayerSpeakEvent += OnSpeak;
        }

        private static List<Player> PatchedPlayer = new List<Player>();

        private void OnSpeak(PlayerSpeakEventArgs ev)
        {
            Player joueur = ev.Player;
            
            if (joueur != null && !PatchedPlayer.Contains(joueur))
            {
                var DissonanceSetupPlayer = joueur.GetComponent<Dissonance.Integrations.MirrorIgnorance.MirrorIgnorancePlayer>();
                DissonanceComms obj = UnityEngine.GameObject.FindObjectOfType<DissonanceComms>();
                //Server.Get.Logger.Info($"Patch {joueur.NickName}");
                if (obj != null && DissonanceSetupPlayer != null)
                {
                    //Server.Get.Logger.Info($"Patch 2");
                    VoicePlayerState playerState = obj.FindPlayer(DissonanceSetupPlayer.PlayerId);
                    //Server.Get.Logger.Info($"Patch 3"); 
                    if (joueur != null && !String.IsNullOrWhiteSpace(joueur?.NickName) && joueur.NickName.StartsWith("War"))
                    {
                        foreach (VoicePlayerState pla in obj.Players)
                        {
                           // Server.Get.Logger.Info($"Patch 4");
                            pla.OnStartedSpeaking += pl =>
                            {
                                if (pl != null)
                                    Server.Get.Logger.Info($"OnStartedSpeaking {pl.Name}");
                                else
                                    Server.Get.Logger.Info($"OnStartedSpeaking null");
                            };
                            PatchedPlayer.Add(joueur);
                        }

                    }
                    if (playerState != null)
                    {
                        Server.Get.Logger.Info($"Patch 4");
                        playerState.OnStartedSpeaking += pl =>
                        {
                            if (pl != null)
                                Server.Get.Logger.Info($"OnStartedSpeaking {pl.Name}");
                        };
                        PatchedPlayer.Add(joueur);
                    }
                }
            }


        //Server.Get.Logger.Info($"OnSpeack {ev.Player?.NickName} - {ev.IntercomTalk} - {ev.RadioTalk} - {ev.Scp939Talk} - {ev.ScpChat} - {ev.SpectatorChat}");
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