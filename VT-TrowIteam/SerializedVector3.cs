using System;
using UnityEngine;

namespace VTTrowItem
{
    [Serializable]
    public class SerializedVector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public SerializedVector3(Vector3 vector)
        {
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }
        public SerializedVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public SerializedVector3()
        {

        }

        public Vector3 Parse()
        {
            return new Vector3(X, Y, Z);
        }
    }
}
