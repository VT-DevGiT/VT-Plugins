using MEC;
using Synapse;
using Synapse.Api;
using Synapse.Config;
using System;
using System.Collections.Generic;
using UnityEngine;
using VT_Referance.Variable;
using VT_Referance.Method;
using Synapse.Api.Events.SynapseEventArguments;
using RemoteAdmin;
using VT_Referance.Behaviour;
using System.Linq;
using Respawning.NamingRules;
using Respawning;

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
            Spawned = false;
            Player.RoleType = RoleType;
            Shield = ActiveComponent<ShieldControler>();

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
            Player.OpenReportWindow(SpawnMessage.Replace("%RoleName%", RoleName).Replace("\\n", "\n"));
            if (SetDisplayInfo)
            {
                Player.RemoveDisplayInfo(PlayerInfoArea.Role);
                List<RoleType> roleWithSquad = new List<RoleType>() { RoleType.FacilityGuard, RoleType.NtfPrivate,
                    RoleType.NtfSergeant, RoleType.NtfCaptain, RoleType.NtfSpecialist};
                
                if (roleWithSquad.Contains(RoleType))
                {
                    Player.RemoveDisplayInfo(PlayerInfoArea.UnitName);
                    Timing.CallDelayed(1f, () => Player.DisplayInfo = $"{RoleName} ({Player.UnitName})" );
                }
                else
                {
                    Player.DisplayInfo = $"{RoleName}";
                }
            }
            if (this is IScpRole)
                Server.Get.Events.Player.PlayerDeathEvent += ScpDeathAnnonce;
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
            Player.MaxHealth = GetConfigValue("MaxHealth", (int)Player.Health);
            Player.Health = GetConfigValue("Health", 100);
            Player.MaxArtificialHealth = GetConfigValue("MaxArtificialHealth", 100);
            Player.ArtificialHP = GetConfigValue<ushort>("ArtificialHealth", 0);
            Shield.MaxShield = GetConfigValue("MaxShield", 0);
            Shield.Shield = GetConfigValue("Shield", 0);
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

        internal void ScpDeathAnnonce(PlayerDeathEventArgs ev)
        {
            if (ev.Victim != Player)
                return;
            string Name = ((IScpRole)this).ScpName;
            Player player = Server.Get.Players.FirstOrDefault(p => p.PlayerId == ev.HitInfo.PlayerId);
            DamageTypes.DamageType damageType = ev.HitInfo.Tool;
            if (damageType == DamageTypes.Tesla)
                NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase($". SCP {Name} SUCCESSFULLY TERMINATED BY AUTOMATIC SECURITY SYSTEM", 0, 0);
            else if (damageType == DamageTypes.Nuke)
                NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase($". SCP {Name} SUCCESSFULLY TERMINATED BY ALPHA WARHEAD", 0, 0);
            else if (damageType == DamageTypes.Decont)
                NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase($". SCP {Name} LOST IN DECONTAMINATION SEQUENCE", 0, 0);
            else
            {
                string cause = "TERMINATION CAUSE UNSPECIFIED";
                if (player != null)
                {
                    
                    switch (player.TeamID)
                    {
                        case (int)TeamID.NTF:
                            UnitNamingRule rule;
                            string Unit = UnitNamingRules.TryGetNamingRule(SpawnableTeamType.NineTailedFox, out rule) ? rule.GetCassieUnitName(player.UnitName) : "UNKNOWN";
                            cause = "CONTAINEDSUCCESSFULLY CONTAINMENTUNIT " + Unit;
                            break;
                        case (int)TeamID.CHI:
                            cause = "BY CHAOSINSURGENCY";
                            break;
                        case (int)TeamID.RSC:
                            cause = "BY FACILITY PERSONNEL";
                            break;
                        case (int)TeamID.CDP:
                            cause = " BY CLASSD PERSONNEL";
                            break;
                        default:
                            cause = "CONTAINMENTUNIT UNKNOWN";
                            break;
                    }
                }
                NineTailedFoxAnnouncer.singleton.ServerOnlyAddGlitchyPhrase($". SCP {Name} SUCCESSFULLY TERMINATED . {cause}", 0, 0);
            }
        }

        #endregion

    }
}
