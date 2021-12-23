using Synapse.Api;
using Synapse.Config;
using System;
using VT_Referance.Method;
using VT_Referance.Behaviour;

[Serializable]
public class SerializedConfigClass
{
    public string Display { get; set; } = null;
    public int ClassID { get; set; } = -1;
    public int? Health { get; set; } = null;
    public int? MaxHealh { get; set; } = null;
    public int? Shiled { get; set; } = null;
    public int? MaxShiled { get; set; } = null;
    public SerializedPlayerInventory Inventory { get; set; }

    public SerializedConfigClass()
    {
       
    }

    public SerializedConfigClass(string display, int id, int? healh, int? maxHealh, int? shiled, int? maxShiled, SerializedPlayerInventory inventory)
    {
        Display = display;
        ClassID = id;
        Health = healh;
        MaxHealh = maxHealh;
        Shiled = shiled;
        MaxShiled = shiled;
        Inventory = inventory;
    }


    public void Apply(Player player)
    {
        var shiledctrl = player.GetOrAddComponent<ShieldControler>();

        if (!string.IsNullOrWhiteSpace(Display))
        {
            player.SetDisplayCustomRole(Display);
        }
        if (Health != null && Health != 0)
        {
            if (player.MaxHealth < (int)MaxHealh)
                player.MaxHealth = (int)MaxHealh;
            player.Health = (int)Health;
        }
        if (MaxHealh != null && MaxHealh != 0)
            player.MaxHealth = (int)MaxHealh;
        if (Shiled != null && Shiled != 0)
        {
            if (shiledctrl.MaxShield < (int)Shiled)
                shiledctrl.MaxShield = (int)Shiled;
            shiledctrl.Shield = (int)Shiled;
        }
        if (MaxShiled != null && MaxShiled != 0)
            shiledctrl.MaxShield = (int)MaxShiled;
        if (Inventory.IsDefined())
            Inventory.Apply(player);
        
    }

    private string ParseDisplay(Player player)
    {
        Display = Display.Replace( "%name%", player.name);
        Display = Display.Replace( "%unitname%", player.UnitName);
        Display = Display.Replace("%name%", player.name);
        Display = Display.Replace("%name%", player.name);
        Display = Display.Replace("%name%", player.name);
        Display = Display.Replace("%name%", player.name);
        return Display;
    }
}