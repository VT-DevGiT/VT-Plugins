using UnityEngine;

namespace VTEscape
{
    public class NTFEscape : BaseEscape
    {
        public override Vector3 Postion => GetComponent<Escape>().worldPosition;

        public override int Radius => Escape.radius;

        public override EscapeType EscapeType => EscapeType.MTF;
    }
}
