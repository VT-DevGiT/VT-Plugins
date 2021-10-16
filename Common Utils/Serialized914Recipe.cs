using Scp914;
using Synapse.Api;
using System;
using System.Collections.Generic;

namespace Common_Utiles
{

    [Serializable]
    public class Serialized914Recipe
    {
        public int ItemID { get; set; }
        public List<int> Rough { get; set; }
        public List<int> Coarse { get; set; }
        public List<int> OneToOne { get; set; }
        public List<int> Fine { get; set; }
        public List<int> VeryFine { get; set; }

        public Serialized914Recipe()
        {

        }

        public Serialized914Recipe(int itemID, List<int> rough, List<int> coarse, List<int> oneToOne, List<int> fine, List<int> veryFine)
        {
            ItemID = itemID;
            Rough = rough;
            Coarse = coarse;
            OneToOne = oneToOne;
            Fine = fine;
            VeryFine = veryFine;
        }

        public List<int> Parse(Scp914KnobSetting setting)
        {
            switch (setting)
            {
                case Scp914KnobSetting.Rough: return Fine;
                case Scp914KnobSetting.Coarse: return Fine;
                case Scp914KnobSetting.OneToOne: return OneToOne;
                case Scp914KnobSetting.Fine: return Fine;
                case Scp914KnobSetting.VeryFine: return VeryFine;
                default: return null;
            }
        }
    }
}