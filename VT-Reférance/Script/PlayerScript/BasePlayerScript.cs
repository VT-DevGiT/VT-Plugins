﻿using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Config;
using System;
using System.Collections.Generic;
using VT_Referance.Behaviour;
using VT_Referance.Method;

namespace VT_Referance.PlayerScript
{
    public abstract class BasePlayerScript : Synapse.Api.Roles.Role
    {

        #region Attributes & Properties
        protected abstract string SpawnMessage{ get; }
        protected virtual bool SetDisplayInfo => true;
        protected abstract List<int> EnemysList { get; }
        protected abstract List<int> FriendsList { get; }
        protected abstract RoleType RoleType { get; }
        protected abstract int RoleTeam { get; }
        protected abstract int RoleId { get; }
        protected abstract string RoleName { get; }
        protected abstract AbstractConfigSection Config { get; } 

        protected ShieldControler Shield;
        public AbstractConfigSection GetConfig() => Config;

        public override List<int> GetEnemiesID() => EnemysList;
        
        public override List<int> GetFriendsID() => FriendsList;

        public override int GetRoleID() => RoleId;

        public override string GetRoleName() => RoleName;

        public override int GetTeamID() => RoleTeam;

        public virtual bool CallPower(int power) => false;
        
        
        internal bool Spawned = false;

        #endregion

        #region Constructors & Destructor
        #endregion

        #region Methods
        protected void InactiveComponent<T>()
            where T : BaseRepeatingBehaviour
        {
            T composant;
            if (Player.gameObject.TryGetComponent<T>(out composant))
                composant.enabled = false;
        }

        protected void KillComponent<T>()
            where T : BaseRepeatingBehaviour
        {
            T composant;
            if (Player.gameObject.TryGetComponent<T>(out composant))
                composant.Kill();
        }

        protected T ActiveComponent<T>()
            where T : UnityEngine.Behaviour
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

        /// <summary>
        /// Get a value from the config of the classe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Name">Config name</param>
        /// <param name="defaultValue">Default value if the config is not found</param>
        /// <returns></returns>
        private T GetConfigValue<T>(string Name, T defaultValue)
        {
            try
            {
                T value = defaultValue;
                if (Config != null && Config.GetType().GetField(Name) != null)
                {
                    value = (T)Config.GetType().GetField(Name).GetValue(this.Config);
                }
                return value;
            }
            catch (Exception e)
            {
                throw new Exception($"{typeof(T).Name} {Name} :\n{e.Message}");
            }
        }

        /// <summary>
        /// call when the class Spawn
        /// </summary>
        [API]
        protected virtual void AditionalInit()
        { }

        /// <summary>
        /// call when the class Spawn for add Event on the class
        /// Warning ! don't forget to untie them when the Despawn class
        /// </summary>
        [API]
        protected virtual void Event()
        { }

        /// <summary>
        /// if you override you know what you are doing. 
        /// if not, look at the code of the basic method.
        /// </summary>
        [API]
        public override void Spawn()
        {
            Event();
            Player.ChangeRoleAtPosition(RoleType);
            Shield = ActiveComponent<ShieldControler>();

            InitPlayer();

            AditionalInit();

            if (!string.IsNullOrEmpty(SpawnMessage))
                Player.OpenReportWindow(SpawnMessage.Replace("%RoleName%", RoleName).Replace("\\n", "\n"));

            if (SetDisplayInfo)
                Player.SetDisplayCustomRole(RoleName);
                //Timing.CallDelayed(0.01f, () => Player.SetDisplayCustomRole(RoleName));

            if (this is IScpRole)
                Server.Get.Events.Player.PlayerDeathEvent += ScpDeathAnnonce;
            Spawned = true;
        }

        private void InitPlayer()
        {
            Player.Inventory.Clear();
            var inventory = GetConfigValue("inventory", new SerializedPlayerInventory());
            if (inventory.IsDefined())
            { 
                foreach(var item in inventory.Items)
                    if (!Server.Get.ItemManager.IsIDRegistered(item.ID))
                    {
                        Server.Get.Logger.Error($"VT-CustomRole : \n Config error in {nameof(Config)} of the role {RoleName} \n unknown Item ID : {item.ID} ! \n you need to change the configuration!");
                        inventory.Items.Remove(item);
                    }
                inventory.Apply(Player);
            }
            Player.Health = GetConfigValue("Health", 100);
            Player.MaxHealth = GetConfigValue("MaxHealth", (int)Player.Health);
            Player.ArtificialHealth = GetConfigValue("ArtificialHealth", 0);
            Player.MaxArtificialHealth = GetConfigValue("MaxArtificialHealth", 100);
            Shield.Shield = GetConfigValue("Shield", 0);
            Shield.MaxShield = GetConfigValue("MaxShield", 100);
            Server.Get.Logger.Info("Test");
            SerializedMapPoint spawnPoint = GetConfigValue<SerializedMapPoint>("SpawnPoint", null);
            try
            {
                if (spawnPoint != null)
                {
                    Server.Get.Logger.Info(spawnPoint != null);
                    Server.Get.Logger.Info(spawnPoint);
                    Server.Get.Logger.Info(spawnPoint.Parse());
                    Server.Get.Logger.Info(spawnPoint.Parse().Position);
                    Player.Position = spawnPoint.Parse().Position;
                }
            }
            catch (Exception e)
            {
                Server.Get.Logger.Error($"Error Config SpawnPoint at {this.RoleName} : {spawnPoint.Room} - {e}");

            }
        }
        
        [API]
        public override void DeSpawn()
        {
            Player.DisplayInfo = null;
            Player.AddDisplayInfo(PlayerInfoArea.Role);
            Player.AddDisplayInfo(PlayerInfoArea.UnitName);
            if (this is IScpRole)
                Server.Get.Events.Player.PlayerDeathEvent -= ScpDeathAnnonce;
            
        }

        public override string ToString() => $"{this.RoleName}({this.RoleId})";
        #endregion

        #region Event
        private void ScpDeathAnnonce(PlayerDeathEventArgs ev)
        {
            if (ev.Victim != Player)
                return;
            string Name = (this as IScpRole)?.ScpName;
            Server.Get.Map.AnnounceScpDeath(Name, ev.HitInfo.GetScpRecontainmentType());            
        }
        #endregion

    }
}
