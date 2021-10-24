using MEC;
using Respawning.NamingRules;
using Synapse;
using Synapse.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VT_Referance.Variable;

using Dissonance;
using Dissonance.Audio.Capture;
using Dissonance.Audio.Codecs;
using Dissonance.Integrations.MirrorIgnorance;
using Dissonance.Networking;
using Mirror;
using NAudio.Wave;
using System.IO;
using System.Net;

namespace VT_Referance.Method
{
    public static class Methods
    {

        /// <summary>
        /// return a player according to the nearest corpse within the limit of a sphere
        /// </summary>
        /// <param name="player">center of the sphere</param>
        /// <param name="rayon">radius of the sphere</param>
        /// <returns>null if the player is not found</returns>
        [API]
        public static Player GetPlayercoprs(Player player, float rayon)
        {
           // Physics.OverlapSphere(player.Position, 3f).Where(e => e.gameObject.GetComponentInParent<Ragdoll>() != null).ToList
            
            
            List<Collider> colliders = Physics.OverlapSphere(player.Position, rayon)
                .Where(e => e.gameObject.GetComponentInParent<Ragdoll>() != null).ToList();
            colliders.Sort((Collider x, Collider y) =>
            {
                return Vector3.Distance(x.gameObject.transform.position, player.Position)
                .CompareTo(Vector3.Distance(y.gameObject.transform.position, player.Position));
            });
 
            if (colliders.Count == 0)
                return null;

            Ragdoll doll = colliders[0].gameObject.GetComponentInParent<Ragdoll>();
            if (doll == null)
                return null;

            Player owner = Server.Get.Players.FirstOrDefault(p => p.PlayerId == doll.owner.PlayerId);
            

            if (owner != null && owner.RoleID == (int)RoleType.Spectator)
            {
                UnityEngine.Object.DestroyImmediate(doll.gameObject, true);
                return owner;
            }
            return null;
        }

        
        /// <summary>
        /// True if the last role of the played is in the SCP team or null if the player had no role
        /// </summary>
        /// <param name="player">the player you want tested</param>
        /// <returns>true if it was, false if not, null if the player is not referenced</returns>
        [API]
        public static bool? IsWasScpRole(Player player)
        {
            if (player != null && Data.PlayerRole.ContainsKey(player))
            {
                int roleId = Data.PlayerRole[player];
                if (_scpRoleVanila.Contains(roleId)) 
                    return true;
                else if (roleId > Synapse.Api.Roles.RoleManager.HighestRole)
                    return Server.Get.RoleManager.GetCustomRole(roleId).GetTeamID() == (int)TeamID.SCP;
                else return false;
            }
            return null;
        }

        private static List<int> _scpRoleVanila = new List<int>()
        { 
            (int)RoleID.Scp0492,(int)RoleID.Scp079,(int)RoleID.Scp096,
            (int)RoleID.Scp106, (int)RoleID.Scp173,(int)RoleID.Scp93953,
            (int)RoleID.Scp93989, (int)RoleID.Scp049
        };

        
        /// <summary>
        /// if there is an active Air Bombardment or which is starting 
        /// </summary>
        [API]
        public static bool isAirBombCurrently = false;

        // Airbomb of SanayaPlugin
        /// <summary>
        /// Start Air Bombardment that detonates grenades all over the outer area
        /// </summary>
        /// <param name="waitforready">time before the start</param>
        /// <param name="limit">if set to -1 it continues indefinitely</param>
        [API]
        public static IEnumerator<float> AirBomb(int waitforready = 7, int limit = -1)
        {
            if (isAirBombCurrently)
                yield break;
            else isAirBombCurrently = true;

            Room OutsideRoom = Server.Get.Map.GetRoom(MapGeneration.RoomName.Outside);

            Map.Get.Cassie("danger . outside zone emergency termination sequence activated .", false);
            yield return Timing.WaitForSeconds(5f);

            while (waitforready > 0)
            {
                PlayAmbientSound(7);
                OutsideRoom.ChangeRoomLightColor(new Color(0.5f, 0, 0));
                yield return Timing.WaitForSeconds(0.5f);
                OutsideRoom.ChangeRoomLightColor(new Color(1, 0, 0));
                yield return Timing.WaitForSeconds(0.5f);
                waitforready--;
            }

            int throwcount = 0;
            while (isAirBombCurrently)
            {
                List<Vector3> randampos = Data.AirbombPos.OrderBy(x => Guid.NewGuid()).ToList();
                foreach (var pos in randampos)
                {
                    Map.Get.SpawnGrenade(pos, Vector3.zero, 0.1f);
                    yield return Timing.WaitForSeconds(0.1f);
                }
                throwcount++;
                if (limit != -1 && limit <= throwcount)
                {
                    isAirBombCurrently = false;
                    break;
                }
                yield return Timing.WaitForSeconds(0.25f);
            }
            OutsideRoom.ChangeRoomLightColor(new Color(1, 0, 0), false);
            Map.Get.Cassie("outside zone termination completed .", false);
            yield break;
        }


        /// <summary>
        /// play ambient sound ​to all players
        /// </summary>
        /// <param name="id">the id of the sound</param>
        [API]
        public static void PlayAmbientSound(int id)
        {
            PlayerManager.localPlayer.GetComponent<AmbientSoundPlayer>().CallMethod("RpcPlaySound", id);
        }


        /// <summary>
        /// Get the total voltage of the generators
        /// </summary>
        /// <returns>1000 for 1 generator engaged</returns>
        [API]
        public static int GetVoltage()
        {
            float totalvoltagefloat = 0;
            foreach (var generator in Server.Get.Map.Generators)
                totalvoltagefloat += generator.generator._currentTime / generator.generator._totalActivationTime * 1000;
            return (int)totalvoltagefloat;
        }


        /// <summary>
        /// Creat new NTF name Unit
        /// </summary>
        [API]
        public static string GenerateNtfUnitName()
        {
            var combi = typeof(UnitNamingRule).GetFieldOrPropertyValue<List<string>>("UsedCombinations");
            string regular;
            do
            {
                var arrayOfValues = typeof(NineTailedFoxNamingRule).GetFieldOrPropertyValue<string[]>("PossibleCodes");
                regular = arrayOfValues[UnityEngine.Random.Range(0, arrayOfValues.Length)] + "-" + UnityEngine.Random.Range(1, 20).ToString("00");
            }
            while (combi.Contains(regular));
            combi.Add(regular);
            return regular;
        }

        /// <summary>
        /// Reset all color of the room light
        /// </summary>
        public static void ResetRoomsLightColor()
        {
            foreach (Room room in SynapseController.Server.Map.Rooms)
                room.ChangeRoomLightColor(new Color(1,0,0), false);
        }

        /// <summary>
        /// Change the color of all room light
        /// </summary>
        /// <param name="color">The new color</param>
        public static void ChangeRoomsLightColor(Color color)
        {
            foreach (Room room in SynapseController.Server.Map.Rooms)
                room.ChangeRoomLightColor(color);
        }

        /// <summary>
        /// if the player can see a gameobject
        /// </summary>
        /// <param name="camera">The tested camera</param>
        /// <param name="obj">The Tested GameObject</param>
        /// <returns>True if hi can see it, false if hi cant</returns>
        [Unstable] // change this to a ray cast ?
        public static bool IsTargetVisible(UnityEngine.Camera camera, GameObject obj)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);
            var point = obj.transform.position;
            foreach (var plan in planes)
            {
                if (plan.GetDistanceToPoint(point) < 0)
                    return false;
            }
            return true;
        }
    }

    public static class Audio
    {
        public static MirrorIgnoranceClient client;
        public static ClientInfo<MirrorConn> СlientInfo;

        public static void Play(Stream stream, float volume) => Play(stream, 999, volume);

        public static void PlayFromFile(string path, float volume) => Play(new FileStream(path, FileMode.Open), volume);

        public static void PlayFromUrl(string url, float volume)
        {
            using (WebClient webClient = new WebClient())
                Play(new MemoryStream(webClient.DownloadData(url)), volume);
        }

        private static void Play(Stream stream, ushort playerid, float volume)
        {
            MirrorIgnoranceCommsNetwork objectOfType1 = UnityEngine.Object.FindObjectOfType<MirrorIgnoranceCommsNetwork>();
            DissonanceComms objectOfType2 = UnityEngine.Object.FindObjectOfType<DissonanceComms>();
            if (objectOfType1.Client == null)
                objectOfType1.StartClient(Unit.None);
            client = objectOfType1.Client;
            IMicrophoneCapture component;
            if (objectOfType2.TryGetComponent(out component))
            {
                if (component.IsRecording)
                    component.StopCapture();
                UnityEngine.Object.Destroy((UnityEngine.Object)component);
            }
            objectOfType1.Mode = (NetworkMode)1;
            MicrophoneModule microphoneModule = (objectOfType2).gameObject.AddComponent<MicrophoneModule>();
            microphoneModule._file = stream;
            objectOfType2._capture.Start(objectOfType1, microphoneModule);
            objectOfType2._capture.MicrophoneName = "StreamedMic";
            СlientInfo = (objectOfType1.Server._clients).GetOrCreateClientInfo(playerid, "MusicBot", new CodecSettings((Codec)1, 48000U, 960), new MirrorConn(NetworkServer.localConnection));
            ClientInfo<MirrorConn> сlientInfo = СlientInfo;
            objectOfType2.IsMuted = false;
            foreach (KeyValuePair<ushort, RoomChannel> keyValuePair in (objectOfType2.RoomChannels)._openChannelsBySubId.ToArray()) objectOfType2.RoomChannels.Close(keyValuePair.Value);
            objectOfType1.Server._clients.LeaveRoom("Null", сlientInfo);
            objectOfType1.Server._clients.LeaveRoom("Intercom", сlientInfo);
            objectOfType1.Server._clients.JoinRoom("Null", сlientInfo);
            objectOfType1.Server._clients.JoinRoom("Intercom", сlientInfo);
            objectOfType2.RoomChannels.Open("Null", false, (ChannelPriority)2, volume);
            objectOfType2.RoomChannels.Open("Intercom", false, (ChannelPriority)2, volume);
        }

        public class MicrophoneModule : MonoBehaviour, IMicrophoneCapture
        {
            private readonly List<IMicrophoneSubscriber> _subscribers = new List<IMicrophoneSubscriber>();
            private readonly WaveFormat _format = new WaveFormat(48000, 1);
            private readonly float[] _frame = new float[960];
            private readonly byte[] _frameBytes = new byte[3840];
            private float _elapsedTime;
            public Stream _file;
            private int _readOffset;
            private bool _stopped = false;

            public bool IsRecording { get; private set; }

            public TimeSpan Latency { get; private set; }

            public WaveFormat StartCapture(string name)
            {
                WaveFormat format;
                if (_file == null || !this._file.CanRead)
                {
                    if (!_stopped && /*Debug ?*/ true)
                    {
                        Server.Get.Logger.Error((object)string.Format("[Audio] _file==null: {0}", (_file == null)));
                        if (_file != null)
                            Server.Get.Logger.Error((object)string.Format("[Audio] _file.CanRead=={0}", _file.CanRead));
                    }
                    this.IsRecording = false;
                    this.Latency = TimeSpan.FromMilliseconds(0.0);
                    format = _format;
                }
                else
                {
                    this._stopped = false;
                    this.IsRecording = true;
                    this.Latency = TimeSpan.FromMilliseconds(0.0);
                    Server.Get.Logger.Info((object)("[Audio] Enabled: " + name));
                    format = this._format;
                }
                return format;
            }

            public void StopCapture()
            {
                IsRecording = false;
                Server.Get.Logger.Info((object)"[Audio] Disabled");
                if (_file != null)
                {
                    _file.Dispose();
                    _file.Close();
                }
                _stopped = true;
                _file = null;
            }

            public void Subscribe(IMicrophoneSubscriber listener) => this._subscribers.Add(listener);

            public bool Unsubscribe(IMicrophoneSubscriber listener) => this._subscribers.Remove(listener);

            public bool UpdateSubscribers()
            {
                bool flag;
                if (_file == null)
                {
                    flag = true;
                }
                else
                {
                    _elapsedTime += Time.unscaledDeltaTime;
                    while (_elapsedTime > 0.0199999995529652)
                    {
                        _elapsedTime -= 0.02f;
                        int count = _file.Read(_frameBytes, 0, _frameBytes.Length);
                        _readOffset += count;
                        Array.Clear(_frame, 0, _frame.Length);
                        Buffer.BlockCopy(_frameBytes, 0, _frame, 0, count);
                        foreach (IMicrophoneSubscriber subscriber in _subscribers)
                            subscriber.ReceiveMicrophoneData(new ArraySegment<float>(_frame), _format);
                    }
                    flag = false;
                }
                return flag;
            }
        }
    }
}
