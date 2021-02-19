using Synapse.Api.Items;
using System;


namespace VTGrenad
{
    public class AmorcableGrenade
    {
        public bool IsFlash { get { return GrItem.ItemType == ItemType.GrenadeFlash; } }
        public SynapseItem GrItem { get; private set; }
        public DateTime DropTime { get; private set; }
        public bool IsArmed
        {
            get
            {
                return (DateTime.Now - DropTime).Seconds >= Plugin.Config.Time;
            }
        }

        public bool Used { get; set; }

        public AmorcableGrenade(SynapseItem gritem)
        {
            GrItem = gritem;
            DropTime = DateTime.Now;
            Used = false;

        }
    }
}
