using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using System;
using System.Reflection;

namespace VT_Referance.ItemScript
{
    [API]
    public abstract class BaseItemScript
    {
        #region Attributes & Properties
        public readonly int ID;
        public readonly ItemType ItemType;
        private readonly string Name;

        public virtual string ScrenName { get; set; } = null;
        public virtual string MessagePickUp { get; set; } = null;
        public virtual string MessageChangeTo { get; set; } = null;

        #endregion

        #region Constructors & Destructor

        [API]
        public BaseItemScript()
        {
            var itemInfo = new ItemInformation(); 
            itemInfo = GetType().GetCustomAttribute<ItemInformation>();
            Name = itemInfo.Name;
            ID = itemInfo.ID;
            ItemType = itemInfo.ItemType;
            Registere();
            Event();
        }
        #endregion

        #region Methods

        private void Registere()
        {
            try
            {
                if (!Server.Get.ItemManager.IsIDRegistered(ID) && ID != -1)
                    Server.Get.ItemManager.RegisterCustomItem(new CustomItemInformation()
                    {
                        ID = ID,
                        BasedItemType = ItemType,
                        Name = Name
                    });
                
                else if (ID == -1)
                    Server.Get.Logger.Error($"Error ! the item {this} failed to be register:\nIt didt ave ItemInformation");
                else if (Server.Get.ItemManager.GetName(ID) == Name)
                    Server.Get.Logger.Error($"Error ! the item {this} failed to be register:\nIt was already registerd");
                else
                    Server.Get.Logger.Error($"Error ! the item {this} failed to be register:\nThe ID {ID} was already registerd");
            }
            catch (Exception e)
            {
                throw new Exception($"Error ! the item {this} failed to be register:\n{e}\n{e}");
            }
        }


        /// <summary>
        ///  for attached additional events 
        ///  Waring, there are already events that are attached
        /// </summary>
        protected virtual void Event()
        {
            Server.Get.Events.Player.PlayerDropItemEvent += OnDrop;
            Server.Get.Events.Player.PlayerItemUseEvent += OnUse;
            Server.Get.Events.Player.PlayerPickUpItemEvent += OnPickUp;
            Server.Get.Events.Player.PlayerChangeItemEvent += OnChangeItem;
        }

        private void OnChangeItem(PlayerChangeItemEventArgs ev)
        {
            if (ev.NewItem?.ID == ID)
                this.ChangeToItem(ev);
            else if (ev.OldItem?.ID == ID)
                this.ChangedFromItem(ev);
        }

        /// <summary>
        /// this method is called when the player have a item but change to this item
        /// </summary>
        /// <param name="ev"></param>
        [Unstable]
        protected virtual void ChangeToItem(PlayerChangeItemEventArgs ev)
        {
            if (!string.IsNullOrEmpty(MessageChangeTo))
            { 
                string message = MessageChangeTo.Replace("%Name%", ScrenName).Replace("\\n", "\n");
                ev.Player.GiveTextHint(message);
            }
        }

        /// <summary>
        /// this method is called when the player have this item but change to an other
        /// </summary>
        /// <param name="ev">The contexte</param>
        [API]
        protected virtual void ChangedFromItem(PlayerChangeItemEventArgs ev)
        { }

        private void OnPickUp(PlayerPickUpItemEventArgs ev)
        {
            if (ev.Item.ID == ID)
                this.PickUp(ev);
        }

        /// <summary>
        /// this method is called when the object is picked up
        /// </summary>
        /// <param name="ev">The contexte</param>
        [Unstable]
        protected virtual void PickUp(PlayerPickUpItemEventArgs ev)
        {
            if (!string.IsNullOrEmpty(MessagePickUp))
            { 
                string message = MessagePickUp.Replace("%Name%", ScrenName).Replace("\\n", "\n");
                ev.Player.GiveTextHint(message);
            }
        }

        private void OnUse(PlayerItemInteractEventArgs ev)
        {
            if (ev.CurrentItem.ID == ID)
                this.Use(ev);
        }

        /// <summary>
        /// this method is called when the object is used
        /// </summary>
        /// <param name="arg">The contexte</param>
        [API]
        protected virtual void Use(PlayerItemInteractEventArgs ev)
        { }

        private void OnDrop(PlayerDropItemEventArgs ev)
        {
            if (ev.Item.ID == ID)
                this.Drop(ev);
        }

        /// <summary>
        /// this method is called when the object is droped
        /// </summary>
        /// <param name="arg">The contexte</param>
        [API]
        protected virtual void Drop(PlayerDropItemEventArgs ev)
        { }

        #endregion
    }
}
