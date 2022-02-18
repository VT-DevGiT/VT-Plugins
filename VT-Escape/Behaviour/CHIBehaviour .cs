using UnityEngine;

namespace VTEscape
{
    public class CHIEscape : BaseEscape
    {
        public override Vector3 Postion => Plugin.Config.ICEscapePostion;

        public override int Radius => 1;

        public override EscapeType EscapeType => EscapeType.CHI;
    }
}

