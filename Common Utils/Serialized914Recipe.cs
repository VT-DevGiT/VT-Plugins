using Synapse.Api;
using System;
using System.Collections.Generic;

[Serializable]
public class Serialized914Recipe
{
    public int ItemID { get; set; }
    public List<int> Rough { get; set; }
    public List<int> Coarse { get; set; }
    public List<int> OneToOne { get; set; }
    public List<int> Fine { get; set; }
    public List<int> VeryFine { get; set; }

    public Serialized914Recipe(Scp914Recipe recipe)
    {
        ItemID = recipe.itemID;
        Rough = recipe.rough;
        Coarse = recipe.coarse;
        OneToOne = recipe.oneToOne;
        Fine = recipe.fine;
        VeryFine = recipe.veryFine;
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

    public Serialized914Recipe()
    {

    }



    public Scp914Recipe Parse()
    {
        return new Scp914Recipe()
        {
            itemID = this.ItemID,
            rough = this.Rough,
            coarse = this.Coarse,
            oneToOne = this.OneToOne,
            fine = this.Fine,
            veryFine = this.VeryFine,
        };

    }
}