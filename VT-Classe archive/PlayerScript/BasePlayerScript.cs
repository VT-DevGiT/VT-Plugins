using CustomClass.Config;
using Synapse.Api.Events.SynapseEventArguments;

namespace CustomClass.PlayerScript
{
    public abstract class BasePlayerScript : Synapse.Api.Roles.Role
    {


        public override System.Collections.Generic.List<int> GetEnemysID() => new System.Collections.Generic.List<int> { (int)Team.CDP, Team.MTF, Team.RSC };

        public override System.Collections.Generic.List<int> GetFriendsID()
        {
            return new System.Collections.Generic.List<int> { (int)Team.SCP };
        }

        internal bool Spawned = false;

        internal int NSpawned = 0;

        internal string oldtag;

        public abstract IBaseConfig Config { get; }
        public abstract MoreClasseID ClasseID { get; }
        public abstract Team ClasseTeam { get; }
        public abstract RoleType ClasseRole { get; }

        #region Event
        public virtual void OnDeathEvent(PlayerDeathEventArgs EventArgs)
        {
            Player.tag = oldtag;
            Player.HideRank = false;
        }

        public virtual void OnKillEvent(PlayerDeathEventArgs eventArgs)
        {

        }

        internal void OnCuffEvent(PlayerCuffTargetEventArgs eventArgs)
        {

        }

        public virtual void OnShootEvent(PlayerShootEventArgs eventArgs)
        {

        }

        internal void OnItemUse(PlayerItemInteractEventArgs eventArgs)
        {

        }

        internal void OnDamageEvent(PlayerDamageEventArgs EventArgs)
        {

        }
        #endregion

        #region Get
        public bool GetCommandAllowed()
        {
            return true;
        }

        public override int GetTeamID()
        {
            return ClasseTeam;
        }
        public override string GetRoleName()
        {
            return Config.ConfigRoleName;
        }

        public override int GetRoleID()
        {
            return (int)ClasseID;
        }
        #endregion
        public abstract void AdditionalInit();

        public override void Spawn()
        {
            Spawned = false;
            Player.RoleType = ClasseRole;

            Player.Inventory.Clear();
            oldtag = Player.tag;
            Player.tag = Config.ConfigRoleName;
            if (Config.CongifShowTag)
                Player.HideRank = false;
            else
                Player.HideRank = true;
            int nbCree = 0;
            foreach (var item in Config.ConfigItems)
            {
                if (nbCree < 8)
                {
                    var obj = item.Parse();
                    if (obj != null)
                    {
                        Player.Inventory.AddItem(obj);
                        nbCree++;
                    }
                }
            }
            Player.Health = Config.ConfigHealth;
            Player.MaxHealth = Config.ConfigHealth;
            Player.ArtificialHealth = Config.ConfigArtificialHealth;
            Player.MaxArtificialHealth = Config.ConfigMaxArtificialHealth;

            AdditionalInit();

            Player.OpenReportWindow(MoreClasseClass.GetTranslation("spawn"));
        }
    }
}
