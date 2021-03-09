using CustomClass.Pouvoir;
using Synapse;
using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CustomClass.PlayerScript
{
    public abstract class BasePlayerScript : Synapse.Api.Roles.Role
    {

        #region Attributes & Properties
        protected virtual bool SetDisplayInfo => true;
        protected abstract List<int> EnemysList { get; }
        protected abstract List<int> FriendsList { get; }
        protected abstract RoleType RoleType { get; }
        protected abstract int RoleTeam { get; }
        protected abstract int RoleId { get; }
        protected abstract string RoleName { get; }
        protected abstract AbstractConfigSection Config { get; }

        public override List<int> GetEnemiesID() => EnemysList;
        
        public override List<int> GetFriendsID() => FriendsList;

        public override int GetRoleID() => RoleId;

        public override string GetRoleName() => RoleName;

        public override int GetTeamID() => RoleTeam;

        public virtual bool CallPower(PowerType power)
        {
            return false;
        }
        
        internal bool Spawned = false;

        public MapPoint MapPoint()
        {
            SerializedMapPoint SpawnPoint = GetConfigValue<SerializedMapPoint>("SpawnPoint", null);
            if (SpawnPoint != null)
            {
                return SpawnPoint.Parse();
            }
            return null;
        }

        #endregion

        #region Constructors & Destructor
        #endregion

        #region Methods
        protected void InactiveComponent<T>()
            where T : Behaviour
        {
            T composant;
            if (Player.gameObject.TryGetComponent<T>(out composant))
                composant.enabled = false;
        }
        protected T GetComponent<T>()
            where T : Behaviour
        {
            T composant;
            if (!Player.gameObject.TryGetComponent<T>(out composant))
            {
                composant = Player.gameObject.AddComponent<T>();
            }
            else
            {
                composant.enabled = true;
            }
            return composant;
        }
        private T GetConfigValue<T>(string Name, T defaultValue)
        {
            T value = defaultValue;
            if (Config != null && Config.GetType().GetField(Name) != null)
            {
                value = (T)Config.GetType().GetField(Name).GetValue(this.Config);
            }
            return value;
        }

        protected virtual void AditionalInit()
        {

        }

        protected virtual void Event()
        {

        }

        public override void Spawn()
        {
            Spawned = false;
            Player.RoleType = RoleType;

            Player.Inventory.Clear();
            var items = GetConfigValue("Items", new List<SerializedItem>());

            foreach (var item in items)
            {
                try
                {
                    var obj = item.Parse();
                    Player.Inventory.AddItem(item.Parse());
                }
                catch(Exception e)
                {
                    Server.Get.Logger.Error($"Error Congif inventory at {this.RoleName} : { item.ID} {e} ");
                }
                
            }
            Player.Ammo5 = GetConfigValue<uint>("Ammo5", 0);
            Player.Ammo7 = GetConfigValue<uint>("Ammo7", 0);
            Player.Ammo9 = GetConfigValue<uint>("Ammo9", 0);
            Player.Health = GetConfigValue("Health", 100);
            Player.MaxHealth = GetConfigValue("MaxHealth", (int)Player.Health);

            Player.MaxArtificialHealth = GetConfigValue("MaxArtificialHealth", 100);
            Player.ArtificialHealth = GetConfigValue("ArtificialHealth", 0);
            SerializedMapPoint spawnPoint = GetConfigValue<SerializedMapPoint>("SpawnPoint", null);
            if (spawnPoint != null)
            {
                try
                {
                    Player.Position = spawnPoint.Parse().Position;
                }
                catch (Exception e)
                {
                    Server.Get.Logger.Error($"Error Congif SpawnPoint at {this.RoleName} : {spawnPoint.Room} - {e}");
                }
            }
            AditionalInit();
            Event();
            Player.OpenReportWindow(PluginClass.PluginTranslation.ActiveTranslation.SpawnMessage.Replace("%RoleName%", RoleName));
            if (SetDisplayInfo)
            {
                Player.RemoveDisplayInfo(PlayerInfoArea.Role);
                Player.DisplayInfo = RoleName;
            }
        }

        public override void DeSpawn()
        {
            Player.DisplayInfo = null;
            Player.AddDisplayInfo(PlayerInfoArea.Role);
        }

        #endregion

    }
}
