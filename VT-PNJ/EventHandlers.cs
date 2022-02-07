using Synapse;

namespace VT_PNJ
{
    public class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundStartEvent += OnStart;
        }

        private void OnStart()
        {
            //NpcDataInit.InitPointForTest();
            //NpcDataInit.ClearNpc();
        }
    }
}