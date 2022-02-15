using UnityEngine;

namespace VTEscape
{
    public class NTFEscape : BaseEscape
    {

        public override Vector3 Postion { get; }

        public override int Radius => Escape.radius;

        public override EscapeType EscapeType => EscapeType.MTF;

        public NTFEscape()
        {
            Postion = GetComponent<Escape>().worldPosition;
        }
    }
}
