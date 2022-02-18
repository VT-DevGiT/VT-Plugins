using HarmonyLib;
using LiteNetLib;
using Mirror.LiteNetLib4Mirror;
using Synapse.Api;
using System;
using System.Text;
using VT_Api.Reflexion;

namespace VT_ModedClientAllow
{
    [HarmonyPatch(typeof(CustomLiteNetLib4MirrorTransport), nameof(CustomLiteNetLib4MirrorTransport.ProcessConnectionRequest))]
    internal class PreAuthenticationPatch
    {
        private static bool Prefix(ConnectionRequest request)
        {
            Logger.Get.Info("PATCH !");
            try
            {
                var exists = request.Data.TryGetByte(out var packetId);
                Logger.Get.Info(exists);
                if (!exists)
                {
                    CustomLiteNetLib4MirrorTransport.RequestWriter.Reset();
                    CustomLiteNetLib4MirrorTransport.RequestWriter.Put(2);
                    request.RejectForce();
                    Logger.Get.Info("Roblox : OOF");
                    return false;
                }
                else
                {
                    Logger.Get.Info(packetId);
                    if (packetId == 5)
                    {
                        Logger.Get.Info(request.Data.GetByte());//attention a cet log !

                        Logger.Get.Info(request.Data.TryGetBytesWithLength(out byte[] debuguidBytes));
                        Logger.Get.Info(debuguidBytes);
                        Logger.Get.Info(request.Data.TryGetBytesWithLength(out byte[] debugjwtBytes));
                        Logger.Get.Info(debugjwtBytes);
                        Logger.Get.Info(request.Data.TryGetBytesWithLength(out byte[] debugnonceBytes));
                        Logger.Get.Info(debugnonceBytes);

                        if (!request.Data.TryGetBytesWithLength(out byte[] uidBytes) ||
                            !request.Data.TryGetBytesWithLength(out byte[] jwtBytes) ||
                            !request.Data.TryGetBytesWithLength(out byte[] nonceBytes))
                        {
                            CustomLiteNetLib4MirrorTransport.RequestWriter.Reset();
                            CustomLiteNetLib4MirrorTransport.RequestWriter.Put(2);
                            request.RejectForce(CustomLiteNetLib4MirrorTransport.RequestWriter);
                            Logger.Get.Info("Roblox : OOFED");
                            return false;
                        }

                        var uid = Encoding.UTF8.GetString(uidBytes);
                        var jwt = Encoding.UTF8.GetString(jwtBytes);
                        var nonce = Encoding.UTF8.GetString(nonceBytes);

                        // TODO When we have a good clien data without the server central use this
                        // to activate the Onconnect event
                        //ClientConnectionData clientConnectionData = ClientConnectionData.DecodeJWT(jwt);

                        int num = CustomNetworkManager.slots;
                        if (LiteNetLib4MirrorCore.Host.ConnectedPeersCount < num)
                        {
                            if (CustomLiteNetLib4MirrorTransport.UserIds.ContainsKey(request.RemoteEndPoint))
                                CustomLiteNetLib4MirrorTransport.UserIds[request.RemoteEndPoint].SetUserId(uid);
                            else
                                CustomLiteNetLib4MirrorTransport.UserIds.Add(request.RemoteEndPoint,
                                    new PreauthItem(uid));

                            // TODO When we have a good clien data without the server central use this
                            // to activate the Onconnect event
                            // SynapseController.ClientManager.Clients[clientConnectionData.Uuid] = clientConnectionData;

                            request.Accept();
                            ServerConsole.AddLog(
                                string.Format("Player {0} preauthenticated from endpoint {1}.", (object)uid,
                                    (object)request.RemoteEndPoint), ConsoleColor.Gray);
                            ServerLogs.AddLog(ServerLogs.Modules.Networking,
                                string.Format("{0} preauthenticated from endpoint {1}.", (object)uid,
                                    (object)request.RemoteEndPoint), ServerLogs.ServerLogType.ConnectionUpdate,
                                false);
                            CustomLiteNetLib4MirrorTransport.PreauthDisableIdleMode();
                            Logger.Get.Info("YYAYAAAA !");
                        }
                        else
                        {
                            CustomLiteNetLib4MirrorTransport.RequestWriter.Reset();
                            CustomLiteNetLib4MirrorTransport.RequestWriter.Put((byte)1);
                            request.Reject(CustomLiteNetLib4MirrorTransport.RequestWriter);
                            Logger.Get.Info("Grrr !");
                        }

                        Logger.Get.Info("Roblox : OUTCH");
                        return false;
                    }
                    else
                    {
                        request.Data.SetField("_position ", 0);
                        request.Data.SetField("_offset", 0);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Get.Error($"Synapse-Client: PreAuthentication Patch failed:\n{e}");
            }

            return true;
        }
    }
}
