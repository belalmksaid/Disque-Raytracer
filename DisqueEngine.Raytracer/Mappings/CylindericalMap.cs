using System;
using System.Collections.Generic;

using System.Text;
using DisqueEngine.Math;

namespace DisqueEngine.Raytracer.Mappings
{
    public class CylindericalMap : Mapping
    {
        public override void GetTexelCoordinates(Vector3 local_hit_point, int xres, int yres, ref int row, ref int column)
        {
            float phi = MathUtil.Atan2(local_hit_point.X, local_hit_point.Z);
            if (phi < 0.0)
                phi += MathUtil.TwoPI;
            float u = phi * MathUtil.InvTwoPI;
            float v = local_hit_point.Y;
            column = (int)(((float)(xres - 1)) * u);
            row = (int)(((float)(yres - 1)) * v);
        }
    }
}
