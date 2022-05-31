using InventorySystem.Configs;
using Mirror;
using Synapse;
using Synapse.Api;


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using VT_Api.Core.Events.EventArguments;
using VT_Api.Core.Items;

namespace VTDevHelp
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundStartEvent += Start;
            //Server.Get.Events.Round.RoundStartEvent += GetsyncVar;
        }

        private void Start()
        {
            Logger.Get.Info(InventoryLimits.Config.CategoryLimits.Count);
            foreach (var limit in InventoryLimits.Config.CategoryLimits)
                Logger.Get.Info(limit);
            Logger.Get.Info("-----");
            Logger.Get.Info(ItemManager.Get.ItemCategoryLimit.Count);
            foreach (var limit in ItemManager.Get.ItemCategoryLimit)
                Logger.Get.Info(limit);

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