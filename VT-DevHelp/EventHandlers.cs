using InventorySystem.Configs;
using MapGeneration;
using Mirror;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using VT_Api.Core.Enum;
using VT_Api.Core.Events.EventArguments;
using VT_Api.Core.Items;
using static Synapse.Api.Events.EventHandler;

namespace VTDevHelp
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundStartEvent += GetRoomName;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamager;
            Server.Get.Events.Round.RoundStartEvent += OnRoundStart;
            Server.Get.Events.Round.TeamRespawnEvent += OnRespawn;
            VtController.Get.Events.Map.Scp914ActivateEvent += OnScp914A;
            VtController.Get.Events.Map.Scp914ChangeSettingEvent += OnScp914C;
            VtController.Get.Events.Map.Scp914UpgradeItemEvent += OnScp914U;
            //Server.Get.Events.Round.RoundStartEvent += GetsyncVar;
        }
        private void OnScp914A(VT_Api.Core.Events.EventArguments.Scp914ActivateEventArgs ev)
        {
            Logger.Get.Info(ev.GetInfo());
        }

        private void OnScp914U(Scp914UpgradeItemEventArgs ev)
        {
            Logger.Get.Info(ev.GetInfo());
        }

        private void OnScp914C(Change914KnobSettingEventArgs ev)
        {
            Logger.Get.Info(ev.GetInfo());        
        }

        private void OnRoundStart()
        {
            Round.Get.NextRespawn = 30;
        }

        private void OnRespawn(TeamRespawnEventArgs ev)
        {
            Round.Get.NextRespawn = 20;
            Logger.Get.Info("Respawn");
            ev.TeamID = (int)TeamID.SHA;
        }

        private void OnDamager(PlayerDamageEventArgs ev)
        {
            if (ev.Killer?.RoleType == RoleType.Scp0492)
                Logger.Get.Info(ev.DamageType);
        }

        private void GetRoomName()
        {
            Synapse.Api.Logger.Get.Info("Rooms Name :");
            foreach (var room in Server.Get.Map.Rooms)
            {
                Synapse.Api.Logger.Get.Info(room.RoomName);
            }
        }

        private void GetsyncVar()
        {
            Dictionary<string, ulong> Variables = new Dictionary<string, ulong>();
            foreach (PropertyInfo property in typeof(ServerConsole).Assembly.GetTypes().SelectMany(x => x.GetProperties()).Where(m => m.Name.StartsWith("Network")))
            {
                MethodInfo setMethod = property.GetSetMethod();
                if (setMethod == null)
                    continue;
                MethodBody methodBody = setMethod.GetMethodBody();
                if (methodBody == null)
                    continue;
                byte[] bytecodes = methodBody.GetILAsByteArray();
                if (!Variables.ContainsKey($"{property.Name}"))
                    Variables.Add($"{property.Name}", bytecodes[bytecodes.LastIndexOf((byte)OpCodes.Ldc_I8.Value) + 1]);
            }

            Dictionary<Type, MethodInfo> Writers = new Dictionary<Type, MethodInfo>();
            foreach (MethodInfo method in typeof(NetworkWriterExtensions).GetMethods().Where(x => !x.IsGenericMethod && x.GetParameters()?.Length == 2))
                Writers.Add(method.GetParameters().First(x => x.ParameterType != typeof(NetworkWriter)).ParameterType, method);

            foreach (MethodInfo method in typeof(GeneratedNetworkCode).GetMethods().Where(x => !x.IsGenericMethod && x.GetParameters()?.Length == 2 && x.ReturnType == typeof(void)))
                Writers.Add(method.GetParameters().First(x => x.ParameterType != typeof(NetworkWriter)).ParameterType, method);

            foreach (Type serializer in typeof(ServerConsole).Assembly.GetTypes().Where(x => x.Name.EndsWith("Serializer")))
            {
                foreach (MethodInfo method in serializer.GetMethods().Where(x => x.ReturnType == typeof(void) && x.Name.StartsWith("Write")))
                    Writers.Add(method.GetParameters().First(x => x.ParameterType != typeof(NetworkWriter)).ParameterType, method);
            }
            

            Logger.Get.Info("All sync variable :");
            foreach(var SyncVarDirtyBit in Variables)
            {
                var log = String.Format("{0,-60} {1,-60}", SyncVarDirtyBit.Key, SyncVarDirtyBit.Value);
                
                Logger.Get.Info(log);
            }

            Logger.Get.Info("All writhers :");
            foreach (var WriterExtension in Writers)
            {
                var @namespace = WriterExtension.Value.DeclaringType.FullName + "." + WriterExtension.Value.Name;
                var log = String.Format("{0,-80} {1,-80} {2,-60}", WriterExtension.Key, WriterExtension.Value.Name, @namespace);  

                Logger.Get.Info(log);
            }
        }
    }
}