using Synapse;
using VT_Referance.Variable.Npc;

namespace VT_PNJ
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Round.RoundStartEvent += OnStart;
        }

        private void OnStart()
        {
            NpcDataInit.InitPointForTest();
            NpcDataInit.ClearNpc();
        }
    }
}