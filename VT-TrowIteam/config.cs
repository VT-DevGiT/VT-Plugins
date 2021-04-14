using Synapse;
using Synapse.Config;
using System.ComponentModel;
using System.Globalization;
using UnityEngine;

namespace VTTrowItem
{
    public class Config : AbstractConfigSection
    {

        [Description("if when a player drops the object it is thrown")]
        public bool DropEvent = true;

        [Description("the key to launch the item or to place it (if DropEvent is true)")]
        public KeyCode key = KeyCode.G;

        [Description("tire configuration")]
        public int ThrowForce = 17;

        internal Vector3 initialPosVec3 = new SerializedVector3(0f, 0.25f, 0f).Parse();
        internal Vector3 addLaunchForce = new SerializedVector3(0f, -0.5f, 0f).Parse();

        [Description("the rotation of the throw")]
        public float RotationMinY = 0f;
        public float RotationMinX = 0f;
        public float RotationMinZ = 0f;
        public float RotationMaxX = 0f;
        public float RotationMaxY = 0f;
        public float RotationMaxZ = 0f;


        private void ParseVector(string value, ref Vector3 output)
        {
            string helper = value.Trim();

            if (helper[0] != '(' || helper[helper.Length - 1] != ')')
                goto Retardation;

            string[] values = helper.Split(',');

            if (values.Length < 3)
                goto Retardation;
            var whatToTrim = new char[] { ' ', '(', ')' };

            try
            {
                for (int i = 0; i < 3; i++)
                {
                    output[i] = float.Parse(values[i].Trim(whatToTrim), CultureInfo.InvariantCulture);
                }
            }
            catch { goto Retardation; }

            return;

            Retardation:
            // Throwing here would imply the configs will probably not be read, so let's avoid it, shall we?
            Server.Get.Logger.Error("Vectors configs MUST be: (X.XX, Y.YY, Z.ZZ) (i.e.: (0, 0.5, 0), and they MUST use a '.').");
            return;
        }

        [Description("if the item is pulled over it moves")]
        public bool ShootMouve = true;

        [Description("if a grenade and shoot it and it is active it explodes")]
        public bool ShootInstantFuse = true;
    }
}
