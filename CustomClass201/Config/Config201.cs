using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class Config201 : AbstractConfigSection
    {
        [Description("The MapPoint where the class should Spawn")]
        public SerializedMapPoint SpawnPoint = new SerializedMapPoint("HCZ_Room3ar", -1.792f, 1.330017f, -0.004005589f);

        [Description("The Amount of Health the class have")]
        public int Health = 100;

        [Description("The Items the class spawns with")]
        public List<SerializedItem> Items = new List<SerializedItem>() { new SerializedItem((int)ItemType.Medkit, 35, 0, 0, 0, Vector3.one) };

        [Description("The Amount of Players Required in order to have the Chanche that the class can spawn")]
        public int RequiredPlayers = 0;

        [Description("The name of the class")]
        public string RoleName = " 201";

        [Description("ArtificialHealthConfig of the class")]
        public int ArtificialHealth = 0;
        public int MaxArtificialHealth = 100;
        public bool LoseArtificialHealth = false;

    }
}
