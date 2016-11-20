using System;
using System.Collections.Generic;
using System.Text;
using DisqueEngine.Math;

namespace DisqueEngine.Raytracer.Textures
{
    public class Texture
    {
        public virtual Vector3 GetColor(ShadeRec sr)
        {
            return Colors.White;
        }
    }
}
