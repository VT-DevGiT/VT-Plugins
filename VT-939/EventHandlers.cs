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
            //Server.Get.Events.Player.PlayerSpeakEvent += OnSpeak;
        }

        private static List<Player> PatchedPlayer = new List<Player>();
        //TesT désactivée car cella fait des bug
        private void OnSpeak(PlayerSpeakEventArgs ev)
        {
            Player joueur = ev.Player;
            
            if (joueur != null && !PatchedPlayer.Contains(joueur))
            {
                var DissonanceSetupPlayer = joueur.GetComponent<Dissonance.Integrations.MirrorIgnorance.MirrorIgnorancePlayer>();
                DissonanceComms obj = UnityEngine.GameObject.FindObjectOfType<DissonanceComms>();
                if (obj != null && DissonanceSetupPlayer != null)
                {
                    VoicePlayerState playerState = obj.FindPlayer(DissonanceSetupPlayer.PlayerId);
                    if (joueur != null && !String.IsNullOrWhiteSpace(joueur?.NickName) && joueur.NickName.StartsWith("War"))
                    {
                        foreach (VoicePlayerState pla in obj.Players)
                        {
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
        }

        private void OnSpawn(PlayerSetClassEventArgs ev)
        {
            if (ev.Role.Is939())
            {
                if (ev.Player.gameObject.TryGetComponent(out Scp939Controller customScp939))
                    customScp939.Kill();

                ev.Player.gameObject.AddComponent<Scp939Controller>();
            }
        }


    }
}