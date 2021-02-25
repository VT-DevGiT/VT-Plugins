using Synapse.Config;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomClass.Config
{
    public class CommandsConfig : AbstractConfigSection
    {

        [Description("The config of disguise command")]
        public List<SerializedItem> ConfigDisguise = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)


        };

        [Description("The config of flash command")]
        public List<SerializedItem> ConfigFlash = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)


        };

        [Description("The config of invis command")]
        public List<SerializedItem> ConfigInvis = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)


        };

        [Description("The config of locate command")]
        public List<SerializedItem> ConfigLocate = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)


        };

        [Description("The config of metamorf command")]
        public List<SerializedItem> ConfigMetamorf = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)


        };

        [Description("The config of necro command")]
        public List<SerializedItem> ConfigNecro = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)


        };

        [Description("The config of noclip command")]
        public List<SerializedItem> ConfigRenfort = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)


        };

        [Description("The config of renfort command")]
        public List<SerializedItem> ConfigNoclip = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)


        };

        [Description("The config of revive command")]
        public List<SerializedItem> ConfigRevive = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)

        };

        [Description("The config of summon command")]
        public List<SerializedItem> ConfigSummon = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)

        };

        [Description("The config of surge command")]
        public List<SerializedItem> ConfigSurge = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.RoboticTacticalUnity, -1, 0, 0, 0, Vector3.one),
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)

        };

        [Description("The config of utils command")]
        public List<SerializedItem> ConfigUtils = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)

        };

        [Description("The config of vent command")]
        public List<SerializedItem> ConfigVent = new List<SerializedItem>() {
            new SerializedItem((int)MoreClasseID.TestClass, -1, 0, 0, 0, Vector3.one)

        };
    }
}
