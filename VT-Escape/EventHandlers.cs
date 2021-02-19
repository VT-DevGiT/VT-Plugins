using Synapse;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;

namespace VTEscape
{
    public class EventHandlers
    {
        private readonly SerializedMapPoint DoorSpawnPos = new SerializedMapPoint("EZ_Shelter", 0.04031754f, 1.495544f, 10.24014f);
        public EventHandlers()
        {
            Server.Get.Events.Player.PlayerSetClassEvent += OnPlayerSetClassEvent;
        }

        private void OnPlayerDamageEvent(PlayerDamageEventArgs ev)
        {

        }
        private void OnPlayerSetClassEvent(PlayerSetClassEventArgs ev)
        {
            if (Plugin.Config.MTFEscapeIsEnabled)
            {
                if (ev.Role == RoleType.Spectator && ev.Player.gameObject.GetComponent<NTFEscape>() != null)
                {
                    ev.Player.gameObject.GetComponent<NTFEscape>()?.Destroy();
                }
                else if (ev.Role != RoleType.Spectator && ev.Player.gameObject.GetComponent<NTFEscape>() == null)
                {
                    ev.Player.gameObject.AddComponent<NTFEscape>();
                }
            }
            if (Plugin.Config.ICEscapeIsEnabled)
            {
                if (ev.Role == RoleType.Spectator && ev.Player.gameObject.GetComponent<CHIEscape>() != null)
                {
                    ev.Player.gameObject.GetComponent<CHIEscape>()?.Destroy();
                }
                else if (ev.Role != RoleType.Spectator && ev.Player.gameObject.GetComponent<CHIEscape>() == null)
                {
                    ev.Player.gameObject.AddComponent<CHIEscape>();
                }
            }
            /*
            if (AdvencedEscape.Config.ShelterIsEnabled)
            {
                if (ev.Role == RoleType.Spectator && ev.Player.gameObject.GetComponent<ICEscape>() != null)
                {
                    ev.Player.gameObject.GetComponent<ICEscape>()?.Destroy();
                }
                else if (ev.Role != RoleType.Spectator && ev.Player.gameObject.GetComponent<ICEscape>() == null)
                {
                    ev.Player.gameObject.AddComponent<ICEscape>();
                }
            }
            */
        }
        private void Waiting()
        {

            if (Plugin.Config.ShelterIsEnabled)
            {
                Synapse.Api.Room shelterRoom = Synapse.Api.Map.Get.GetRoom(RoomInformation.RoomType.EZ_SHELTER);


                //Synapse.Api.Door shelterDoor = Synapse.Api.Map.Get.GetDoor(Synapse.Api.Enum.DoorType. );

                //var door = Synapse.Api.Map.Get.SpawnDoorVariant(DoorSpawnPos.Parse().Position, Synapse.Api.Map.Get.GetRoom(RoomInformation.RoomType.EZ_SHELTER).GameObject.transform.rotation);

            }
        }
    }
}