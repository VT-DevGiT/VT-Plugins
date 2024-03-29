﻿using UnityEngine;

namespace VTEscape
{
    public class CHIEscape : BaseEscape
    {
        public override Vector3 Postion => Plugin.Instance.Config.ICEscapePostion;

        public override int Radius => 2;

        public override EscapeType EscapeType => EscapeType.CHI;
    }
}

