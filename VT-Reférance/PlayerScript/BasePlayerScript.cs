using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance;
using VT_Referance.Variable;
using VT_Referance.Method;
using Synapse.Api.Events.SynapseEventArguments;
using RemoteAdmin;
using VT_Referance.Behaviour;

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

        public AbstractConfigSection GetConfig() => Config;

        public override List<int> GetEnemiesID() => EnemysList;
        
        public override List<int> GetFriendsID() => FriendsList;

        public override int GetRoleID() => RoleId;

        public override string GetRoleName() => RoleName;

        public override int GetTeamID() => RoleTeam;

        public virtual bool CallPower(int power)
        {
            return false;
        }
        
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
            T value = defaultValue;
            if (Config != null && Config.GetType().GetField(Name) != null)
            {
                value = (T)Config.GetType().GetField(Name).GetValue(this.Config);
            }
            return value;
        }

        protected virtual void AditionalInit()
        { }

        protected virtual void Event()
        { }
        public override void Spawn()
        {

            Spawned = false;
            Player.RoleType = RoleType;

            if (RoleTeam == (int)TeamID.CHI || RoleTeam == (int)TeamID.NTF)
                Timing.CallDelayed(1f, () => InitPlayer());
            else InitPlayer();
            

            SerializedMapPoint spawnPoint = GetConfigValue<SerializedMapPoint>("SpawnPoint", null);
            if (spawnPoint != null)
            {
                try
                {
                    Player.Position = spawnPoint.Parse().Position;
                }
                catch (Exception e)
                {
                    Server.Get.Logger.Error($"Error Config SpawnPoint at {this.RoleName} : {spawnPoint.Room} - {e}");
                }
            }
            AditionalInit();
            Event();
            Player.OpenReportWindow(SpawnMessage.Replace("%RoleName%", RoleName).Replace("\\n", "\n"));
            if (SetDisplayInfo)
            {
                Player.RemoveDisplayInfo(PlayerInfoArea.Role);
                List<RoleType> roleWithSquad = new List<RoleType>() { RoleType.FacilityGuard, RoleType.NtfCadet,
                    RoleType.NtfLieutenant, RoleType.NtfCommander, RoleType.NtfScientist};
                
                if (roleWithSquad.Contains(RoleType))
                {
                    Player.RemoveDisplayInfo(PlayerInfoArea.UnitName);
                    Timing.CallDelayed(1f, () => Player.DisplayInfo = $"{RoleName} ({Player.UnitName})" );
                }
                else
                {
                    Player.DisplayInfo = $"{RoleName}";
                }
                Player.GetComponent<NicknameSync>().SetField<string>("_displayName", "<color=#855439>Toto</color>");
            }
            if (this is IScpRole)
                Server.Get.Events.Player.PlayerDeathEvent += ScpDeathAnnonce;
        }

        private void InitPlayer()
        {
            Player.Inventory.Clear();
            if (GetConfigValue("inventory", new SerializedPlayerInventory()).IsDefined())
                GetConfigValue("inventory", new SerializedPlayerInventory()).Apply(Player);
            Player.Health = GetConfigValue("Health", 100);
            Player.MaxHealth = GetConfigValue("MaxHealth", (int)Player.Health);
            Player.MaxArtificialHealth = GetConfigValue("MaxArtificialHealth", 100);
            Player.ArtificialHealth = GetConfigValue("ArtificialHealth", 0);
        }
        public override void DeSpawn()
        {
            Player.DisplayInfo = null;
            Player.AddDisplayInfo(PlayerInfoArea.Role);
            Player.AddDisplayInfo(PlayerInfoArea.UnitName);
            if (this is IScpRole)
                Server.Get.Events.Player.PlayerDeathEvent -= ScpDeathAnnonce;
        }

        internal void ScpDeathAnnonce(PlayerDeathEventArgs ev)
        {
            Role role = new Role();
            role.fullName = ((IScpRole)this).ScpName;
            role.roleId = (RoleType)RoleId;

            GameObject gameObject = null;
            foreach (GameObject player in PlayerManager.players)
            {
                if (player.GetComponent<QueryProcessor>().PlayerId == ev.HitInfo.PlayerId)
                    gameObject = player;
            }
            if (gameObject != null)
            {
                NineTailedFoxAnnouncer.AnnounceScpTermination(role, ev.HitInfo, string.Empty);
            }
            else
            {
                DamageTypes.DamageType damageType = ev.HitInfo.GetDamageType();
                if (damageType == DamageTypes.Tesla)
                    NineTailedFoxAnnouncer.AnnounceScpTermination(role, ev.HitInfo, "TESLA");
                else if (damageType == DamageTypes.Nuke)
                    NineTailedFoxAnnouncer.AnnounceScpTermination(role, ev.HitInfo, "WARHEAD");
                else if (damageType == DamageTypes.Decont)
                    NineTailedFoxAnnouncer.AnnounceScpTermination(role, ev.HitInfo, "DECONTAMINATION");
                else
                    NineTailedFoxAnnouncer.AnnounceScpTermination(role, ev.HitInfo, "UNKNOWN");
            }
        }

        #endregion

    }
}
