using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisqueEngine.Math;

namespace DisqueEngine.Raytracer.Textures
{
    public class SphereTextures : Texture
    {
        int num_horizontal_checkers;
        int num_vertical_checkers;
        Texture texture1;
        Texture texture2;

        public void SetTextures(Texture t1, Texture t2)
        {
            texture1 = t1;
            texture2 = t2;
        }

        public void SetCheckerNumbers(int nh, int nv)
        {
            num_horizontal_checkers = nh;
            num_vertical_checkers = nv;
        }

        public override Vector3 GetColor(ShadeRec sr)
        {
            float x = sr.LocalHitPoint.X;
            float y = sr.LocalHitPoint.Y;
            float z = sr.LocalHitPoint.Z;
            float theta =  MathUtil.Acos(y);
            float phi = MathUtil.Atan2(x, z);
            if (phi < 0.0)
                phi += 2.0f * MathUtil.PI;
            float phi_size = 2 * MathUtil.PI / num_horizontal_checkers;
            float theta_size = MathUtil.PI / num_vertical_checkers;
            int i_phi = MathUtil.Floor(phi / phi_size);
            int i_theta = MathUtil.Floor(theta / theta_size);
            float f_phi = phi / phi_size - i_phi;
            float f_theta = theta / theta_size - i_theta;
            if ((i_phi + i_theta) % 2 == 0)
                return (texture1.GetColor(sr));
            else
                return (texture2.GetColor(sr));
        }
    }
}
