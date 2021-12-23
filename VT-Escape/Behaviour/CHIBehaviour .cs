using MEC;
using Synapse;
using Synapse.Api;
using System.Linq;
using UnityEngine;
using VT_Referance.Behaviour;

namespace VTEscape
{
    public class CHIEscape : BaseEscape
    {
        public override Vector3 Postion => new Vector3(-56.2f, 988.9f, -49.6f);

        public override int Radius => 1;

        public override EscapeType EscapeType => EscapeType.CHI;
    }
}

