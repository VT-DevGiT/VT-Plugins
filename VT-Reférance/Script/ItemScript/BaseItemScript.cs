using InventorySystem.Items;
using Synapse;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using Synapse.Api.Items;
using System.Collections.Generic;
using System.Linq;
using VT_Referance.Method;

namespace VT_Referance.ItemScript
{
    [API]
    public abstract class BaseItemScript : ItemBase
    {
        #region Attributes & Properties
        private string _Name;

        public readonly int ID;
        public readonly ItemType ItemType;

        public string Name 
        {   
            get => _Name;
            set 
            { 
                if (value == _Name) return; 
                Server.Get.ItemManager.GetFieldValueorOrPerties<List<CustomItemInformation>>("customItems")
                                      .FirstOrDefault(i => i.ID == this.ID).Name = value; 
               _Name = value; 
            }
        }
        public string MessagePickUp { get; set; } = null;
        public string MessageChangeTo { get; set; } = null;

        #endregion

        #region Constructors & Destructor

        [API]
        public BaseItemScript()
        {
            //if (ID == -1) throw new System.Exception($"Error ! the item failed to be register: \n ID of a item is not found \n It was in the class {}");
            Server.Get.ItemManager.RegisterCustomItem(new CustomItemInformation()
            {
                ID = this.ID,
                BasedItemType = this.ItemType,
                Name = this.Name
            });
            Event();
        }
        #endregion

        #region Methods
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
        [API]
        protected virtual void ChangeToItem(PlayerChangeItemEventArgs ev)
        {
            if (!string.IsNullOrEmpty(MessageChangeTo))
            { 
                string message = MessageChangeTo.Replace("%Name%", Name).Replace("\\n", "\n");
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
        [API]
        protected virtual void PickUp(PlayerPickUpItemEventArgs ev)
        {
            if (!string.IsNullOrEmpty(MessagePickUp))
            { 
                string message = MessagePickUp.Replace("%Name%", Name).Replace("\\n", "\n");
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
