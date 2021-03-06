﻿using System;
using System.Collections.Generic;
using System.Text;
using DisqueEngine.Math;

namespace DisqueEngine.Raytracer.Textures
{
    public class ConstantColor : Texture
    {
        public ConstantColor(Vector3 color)
        {
            Color = color;
        }

        public ConstantColor() { }

        public Vector3 Color = Colors.White;

        public override Vector3 GetColor(ShadeRec sr)
        {
            return Color;
        }
    }
}
