using System;
using System.Collections.Generic;

using System.Text;
using DisqueEngine.Math;

namespace DisqueEngine.Raytracer.Mappings
{
    public enum MapType
    {
        LightProbe, Panoramic
    }
    public class LightProbeMap : Mapping
    {
        public MapType Type;
        public override void GetTexelCoordinates(Vector3 local_hit_point, int xres, int yres, ref int row, ref int column)
        {
            float x = local_hit_point.X;
            float y = local_hit_point.Y;
            float z = local_hit_point.Z;
            float d = MathUtil.Sqrt(x * x + y * y);
            float sin_beta = y / d;
            float cos_beta = x / d;
            float alpha = 0;
            if (Type == MapType.LightProbe)
                alpha = MathUtil.Acos(z);
            if (Type == MapType.Panoramic)
                alpha = MathUtil.Acos(-z);
            float r = alpha * MathUtil.InvPI;
            float u = (1.0f + r * cos_beta) * 0.5f;
            float v = (1.0f + r * sin_beta) * 0.5f;
            column = (int)(((float)(xres - 1)) * u);
            row = (int)(((float)(yres - 1)) * v);
        }
    }
}
