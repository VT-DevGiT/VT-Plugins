using Synapse.Api;
using Synapse.Config;
using System.Collections.Generic;

namespace CustomClass.PlayerScript
{
    public abstract class BasePlayerScript : Synapse.Api.Roles.Role
    {

        #region Attributes & Properties
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

        public override void Spawn()
        {
            Spawned = false;
            Player.RoleType = RoleType;

            Player.Inventory.Clear();
            var items = GetConfigValue("Items", new List<SerializedItem>());

            foreach (var item in items)
            {
                Player.Inventory.AddItem(item.Parse());
            }

            Player.MaxHealth = GetConfigValue("Health", 100);
            Player.Health = Player.MaxHealth;

            Player.MaxArtificialHealth = GetConfigValue("MaxArtificialHealth", Player.MaxHealth);
            Player.ArtificialHealth = GetConfigValue("ArtificialHealth", 0);

            AditionalInit();
            Player.OpenReportWindow($"<color=blue><b>You are now</b></color> <color=red><b>{RoleName}</b></color>");
        }

        public override void DeSpawn()
        {
        }

        #endregion

    }
}
