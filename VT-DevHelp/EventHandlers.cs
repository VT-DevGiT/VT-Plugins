using PlayerStatsSystem;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System;
using VT_Referance.Variable;

namespace VTDevHelp
{
    internal class EventHandlers
    {
        public EventHandlers()
        {
            Server.Get.Events.Server.UpdateEvent += OnUpdate;
            Server.Get.Events.Player.PlayerSetClassEvent += OnSetClass;
            Server.Get.Events.Player.PlayerShootEvent += OnShoot;
            Server.Get.Events.Player.PlayerDamageEvent += OnDamage;
            //Server.Get.Events.Player.LoadStatModulesEvent += OnLoadModules;
        }

        private void OnDamage(PlayerDamageEventArgs ev)
        {
            Server.Get.Logger.Info($"OnDamage\nev.DamageType == {ev.DamageType}\nev.WeaponType == {ev.WeaponType}\nScpRecontainementType == {ev.DamageType.GetScpRecontainmentType()}");
        }

        /*        private void OnLoadModules(LoadStatModulesArgs ev)
                {
                    Server.Get.Logger.Info($"Player {ev.Player.GetPlayer().UserId} is loading Modules");
                }*/

        private void OnShoot(PlayerShootEventArgs ev)
        {
            UnityEngine.GameObject item = UnityEngine.Physics.Raycast(ev.Player.Hub.PlayerCameraReference.transform.position, ev.Player.Hub.PlayerCameraReference.transform.forward, out var hitInfo, 100f, (int)LayerID.Door, UnityEngine.QueryTriggerInteraction.Collide) ? hitInfo.transform.gameObject : null; ;
            Logger.Get.Info($"Debug :\n {item?.name}\n {item?.tag}\n {item?.layer}");
        }

        private void OnSetClass(PlayerSetClassEventArgs ev)
        {
            Logger.Get.Info($"Player Set class : {ev.Player} {ev.Player.RoleID} -> {(int)ev.Role}");
        }

        private void OnUpdate()
        {
            foreach (var player in Server.Get.Players)
            {
                //player.MaxHealth = 250;
                //player.MaxArtificialHealth = 250;
            }
        }
    }
}