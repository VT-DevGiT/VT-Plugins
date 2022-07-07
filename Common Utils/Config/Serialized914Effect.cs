using Scp914;
using Synapse;
using Synapse.Api;
using Synapse.Api.Enum;
using System;
using System.Collections.Generic;
using UnityEngine;
using Logger = Synapse.Api.Logger;

namespace Common_Utiles.Config
{
    [Serializable]
    public class Serialized914Effect
    {
        public int RoleID { get; set; }
        public int RoughEffect { get; set; }
        public int CorseEffect { get; set; }
        public int OneToOneEffect { get; set; }
        public int FineEffect { get; set; }
        public int VeryFineEffect { get; set; }
        public int Chance { get; set; }

        public Serialized914Effect() { }

        public Serialized914Effect(int roleID, int chance, int roughEffect, int corseEffect, int oneToOneEffect, int fineEffect, int veryFineRoleID)
        {
            RoleID = roleID;
            Chance = chance;
            RoughEffect = roughEffect;
            CorseEffect = corseEffect;
            OneToOneEffect = oneToOneEffect;
            FineEffect = fineEffect;
            VeryFineEffect = veryFineRoleID;
        }

        public void Apply(Player player, Scp914KnobSetting setting)
        {
            if ((player.RoleID == RoleID || RoleID == -1) && UnityEngine.Random.Range(0, 100) <= Chance)
            {

                List<int> PositiveEffect = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8  };
                List<int> SuperEffect = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

                switch (setting)
                {
                    case Scp914KnobSetting.Rough:
                        player.GiveEffect((Effect)RoughEffect);
                        return;
                    case Scp914KnobSetting.Coarse:
                        player.GiveEffect((Effect)CorseEffect);
                        return;
                    case Scp914KnobSetting.OneToOne:
                        player.GiveEffect((Effect)OneToOneEffect);
                        return;
                    case Scp914KnobSetting.Fine:
                        if (FineEffect >= 0 && FineEffect <= 33)
                        {
                            player.GiveEffect((Effect)FineEffect);
                        }
                        else if (FineEffect == -2)
                        {
                            if (player.GetData("poseffect") == null || player.GetData("poseffect") == "false")
                            {
                                int poseff = PositiveEffect[UnityEngine.Random.Range(1, PositiveEffect.Count - 1)];
                                switch (poseff)
                                {
                                    case 1:
                                        player.MaxHealth += 50;
                                        player.Health += 50;
                                        player.GiveTextHint("HP+");
                                        player.SetData("poseffect", "true 1");
                                        break;
                                    case 2:
                                        player.GiveEffect(Effect.Invigorated);
                                        player.GiveTextHint("Infinite Stamina");
                                        player.SetData("poseffect", "true 2");
                                        break;
                                    case 3:
                                        player.GiveEffect(Effect.Visuals939);
                                        player.GiveTextHint("939");
                                        player.SetData("poseffect", "true 3");
                                        break;
                                    case 4:
                                        float newScaleX = UnityEngine.Random.Range(Plugin.Instance.Config.Min914SizeX, Plugin.Instance.Config.Max914SizeX);
                                        float newScaleY = UnityEngine.Random.Range(Plugin.Instance.Config.Min914SizeY, Plugin.Instance.Config.Max914SizeY);
                                        float newScaleZ = UnityEngine.Random.Range(Plugin.Instance.Config.Min914SizeZ, Plugin.Instance.Config.Max914SizeZ);
                                        player.Scale = new Vector3(newScaleX, newScaleY, newScaleZ);
                                        player.GiveTextHint("Your size changed");
                                        player.SetData("poseffect", "true 4");
                                        break;
                                    case 5:
                                        player.MaxArtificialHealth += 50;
                                        player.ArtificialHealth += 50;
                                        player.GiveTextHint("AHP+");
                                        player.SetData("poseffect", "true 5");
                                        break;
                                    case 6:                                        
                                        player.GiveTextHint("Aléatruisme");
                                        player.SetData("poseffect", "true 6");
                                        break;
                                    case 7:
                                        player.GiveEffect(Effect.DamageReduction, 50);
                                        player.GiveTextHint("Strong skin");
                                        player.SetData("poseffect", "true 7");
                                        break;
                                    case 8:
                                        Server.Get.Host.ExecuteCommand($"effect {player.PlayerId} SCP1853=1");
                                        player.GiveEffect(Effect.Invigorated);
                                        player.GiveTextHint("Better With weapon");
                                        player.SetData("poseffect", "true 8");
                                        break;
                                }
                            }
                            else
                            {
                                var effect = player.GetData("poseffect").Split();
                                if (!effect[0].Equals("false"))
                                {
                                    int eff = int.Parse(effect[1]);
                                    switch (eff)
                                    {
                                        case 1:
                                            player.MaxHealth -= 50;
                                            player.Health -= 50;
                                            player.SetData("poseffect", "false");
                                            break;
                                        case 2:
                                            Logger.Get.Info(player.StaminaUsage);
                                            player.GiveEffect(Effect.Invigorated, 0);
                                            player.SetData("poseffect", "false");
                                            break;
                                        case 3:
                                            player.GiveEffect(Effect.Visuals939, 0);
                                            player.SetData("poseffect", "false");
                                            break;
                                        case 4:
                                            player.Scale = new Vector3(1, 1, 1);
                                            player.SetData("poseffect", "false");
                                            break;
                                        case 5:
                                            player.MaxArtificialHealth -= 50;
                                            player.ArtificialHealth -= 50;
                                            player.SetData("poseffect", "false");
                                            break;
                                        case 6:
                                            player.SetData("poseffect", "false");
                                            break;
                                        case 7:
                                            player.GiveEffect(Effect.DamageReduction, 0);
                                            player.SetData("poseffect", "false");
                                            break;
                                        case 8:
                                            Server.Get.Host.ExecuteCommand($"effect {player.PlayerId} SCP1853=0");
                                            player.GiveEffect(Effect.Invigorated, 0);
                                            player.SetData("poseffect", "false");
                                            break;
                                    }
                                }
                            }
                        }
                        return;                        
                    case Scp914KnobSetting.VeryFine:
                        if (VeryFineEffect >= 0 && VeryFineEffect <= 33)
                        {
                            player.GiveEffect((Effect)VeryFineEffect);
                        }
                        else if (VeryFineEffect == -3)
                        {
                            Logger.Get.Info("hehe");
                        hehe:
                            if (player.GetData("supeffect") == null || player.GetData("supeffect") == "false")
                            {
                                
                                int supeff = SuperEffect[UnityEngine.Random.Range(1, SuperEffect.Count - 1)];
                                switch (supeff)
                                {
                                    case 1:
                                        player.MaxHealth += 150;
                                        player.Health += 150;
                                        player.GiveTextHint("HP+++");
                                        player.SetData("supeffect", "true 1");
                                        break;
                                    case 2:
                                        player.GiveEffect(Effect.MovementBoost, 255);
                                        player.GiveEffect(Effect.Invigorated, 1);
                                        player.GiveTextHint("NYYYOM");
                                        player.SetData("supeffect", "true 2");
                                        break;
                                    case 3:
                                        player.GiveEffect(Effect.Visuals939);
                                        player.GiveTextHint("939");
                                        player.SetData("supeffect", "true 3");
                                        break;
                                    case 4:
                                        float newScaleX = UnityEngine.Random.Range(Plugin.Instance.Config.Min914SizeX, Plugin.Instance.Config.Max914SizeX);
                                        float newScaleY = UnityEngine.Random.Range(Plugin.Instance.Config.Min914SizeY, Plugin.Instance.Config.Max914SizeY);
                                        float newScaleZ = UnityEngine.Random.Range(Plugin.Instance.Config.Min914SizeZ, Plugin.Instance.Config.Max914SizeZ);
                                        player.Scale = new Vector3(newScaleX, newScaleY, newScaleZ);
                                        player.GiveTextHint("Your Size changed");
                                        player.SetData("supeffect", "true 4");
                                        break;
                                    case 5:
                                        player.MaxArtificialHealth += 150;
                                        player.ArtificialHealth += 150;
                                        player.GiveTextHint("AHP+++");
                                        player.SetData("supeffect", "true 5");
                                        break;
                                    case 6:
                                        player.Invisible = true;
                                        player.GiveTextHint("Invisible");
                                        player.SetData("supeffect", "true 6");
                                        break;
                                    case 7:
                                        player.GiveEffect(Effect.DamageReduction, 100);
                                        player.GiveTextHint("Chuck Norris");
                                        player.SetData("supeffect", "true 7");
                                        break;
                                    case 8:
                                        player.SetData("1up", "true");
                                        player.GiveTextHint("Green Mushroom ?!");
                                        player.SetData("supeffect", "true 8");
                                        break;
                                }
                                player.GiveEffect(Effect.Bleeding);
                            }
                            else
                            {
                                Logger.Get.Info("else");
                                var effect = player.GetData("supeffect").Split();
                                Logger.Get.Info(effect[0] + " " + effect[1]);
                                if (!effect[0].Equals("false"))
                                {
                                    int eff = int.Parse(effect[1]);
                                    Logger.Get.Info(eff);
                                    switch (eff)
                                    {
                                        case 1:
                                            player.MaxHealth -= 150;
                                            player.Health -= 150;
                                            player.SetData("supeffect", "false");
                                            break;
                                        case 2:
                                            player.GiveEffect(Effect.MovementBoost, 0);
                                            player.GiveEffect(Effect.Invigorated, 0);
                                            player.SetData("supeffect", "false");
                                            break;
                                        case 3:
                                            player.GiveEffect(Effect.Visuals939, 0);
                                            player.SetData("supeffect", "false");
                                            break;
                                        case 4:
                                            player.Scale = new Vector3(1, 1, 1);
                                            player.SetData("supeffect", "false");
                                            break;
                                        case 5:
                                            player.MaxArtificialHealth -= 150;
                                            player.ArtificialHealth -= 150;
                                            player.SetData("supeffect", "false");
                                            break;
                                        case 6:
                                            player.Invisible = false;
                                            player.SetData("supeffect", "false");
                                            break;
                                        case 7:
                                            player.GiveEffect(Effect.DamageReduction, 0);
                                            player.SetData("supeffect", "false");
                                            break;
                                        case 8:
                                            player.SetData("1up", "false");
                                            player.SetData("supeffect", "false");
                                            break;
                                    }
                                    
                                }
                                player.GiveEffect(Effect.Bleeding, 0);
                                goto hehe;
                            }                            
                        }
                        return;
                }
            }
        }
    }
}