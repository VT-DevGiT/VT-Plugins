using Mirror;
using Synapse;
using Synapse.Api;

namespace VTDevHelp
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Logger.Get.Info("init EventHandlers");
            Server.Get.Events.Round.RoundStartEvent += OnStart;
        }

        private void OnStart()
        {
            Logger.Get.Info("Spawble Prefabs :");
            foreach (var pre in NetworkManager.singleton.spawnPrefabs)
                Logger.Get.Info(pre. name);
        }
    }
}