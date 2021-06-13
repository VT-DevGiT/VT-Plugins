using Synapse.Api;
using Synapse.Config;
using System;
using VT_Referance.Method;
using VT_Referance.Behaviour;

[Serializable]
public class SerializedConfigClass
{
    public int ClassID { get; set; } = -1;
    public int? Health { get; set; } = null;
    public int? MaxHealh { get; set; } = null;
    public int? Shiled { get; set; } = null;
    public int? MaxShiled { get; set; } = null;
    public SerializedPlayerInventory Inventory { get; set; }

    public SerializedConfigClass()
    {
       
    }

    public SerializedConfigClass(int id, int? healh, int? maxHealh, int? shiled, int? maxShiled, SerializedPlayerInventory inventory)
    {
        ClassID = id;
        Health = healh;
        MaxHealh = maxHealh;
        Shiled = shiled;
        MaxShiled = shiled;
        Inventory = null;
    }


    public void Applied(Player player)
    {
        ShieldControler shiledctrl;
        if (player.gameObject.TryGetComponent(out shiledctrl))
            shiledctrl = player.gameObject.AddComponent<ShieldControler>();

        if (Health != null)
        {
            if (player.MaxHealth < (int)MaxHealh)
                player.MaxHealth = (int)MaxHealh;
            player.Health = (int)Health;
        }
        if (MaxHealh != null)
            player.MaxHealth = (int)MaxHealh;
        if (Shiled != null)
        {
            if (shiledctrl.MaxShield < (int)Shiled)
                shiledctrl.MaxShield = (int)Shiled;
            shiledctrl.Shield = (int)Shiled;
        }
        if (MaxShiled != null)
            shiledctrl.MaxShield = (int)MaxShiled;
        if (Inventory.IsDefined() && Inventory != null)
            Inventory.Apply(player);
    }
}