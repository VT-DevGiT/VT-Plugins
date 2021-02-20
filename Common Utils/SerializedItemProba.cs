using Synapse.Api;
using System;
using UnityEngine;

[Serializable]
public class SerializedItemProba
{
    public SerializedItemProba(Synapse.Api.Items.SynapseItem item)
    {
        ID = item.ID;
        Durabillity = item.Durabillity;
        Barrel = item.Barrel;
        Sight = item.Sight;
        Other = item.Other;
        XSize = item.Scale.x;
        YSize = item.Scale.y;
        ZSize = item.Scale.z;
        Proba = 0;
    }

    public SerializedItemProba(int id, float durabillity, int barrel, int sight, int other, Vector3 scale, float proba)
    {
        ID = id;
        Durabillity = durabillity;
        Barrel = barrel;
        Sight = sight;
        Other = other;
        XSize = scale.x;
        YSize = scale.y;
        ZSize = scale.z;
        Proba = proba;
    }

    public SerializedItemProba()
    {

    }
    public float Proba { get; set; }
    public int ID { get; set; }
    public float Durabillity { get; set; }
    public int Sight { get; set; }
    public int Barrel { get; set; }
    public int Other { get; set; }
    public float XSize { get; set; }
    public float YSize { get; set; }
    public float ZSize { get; set; }

    public Synapse.Api.Items.SynapseItem Parse()
    {
        System.Random rand = new System.Random();
        if (rand.Next(0, 100) <= Proba)
        {
          return new Synapse.Api.Items.SynapseItem(ID, Durabillity, Sight, Barrel, Other) { Scale = new Vector3(XSize, YSize, ZSize) };
        }
        else
        {
            return null;
        }
    }
}