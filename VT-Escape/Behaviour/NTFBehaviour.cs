using MEC;
using Synapse;
using Synapse.Api;
using System.Linq;
using UnityEngine;
using VT_Referance.Behaviour;

namespace VTEscape
{
    public class NTFEscape : BaseEscape
    {
        public override Vector3 Postion => GetComponent<Escape>().worldPosition;

        public override int Radius => Escape.radius;

        public override EscapeType EscapeType => throw new System.NotImplementedException();
    }
}
