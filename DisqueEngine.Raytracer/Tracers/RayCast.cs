using System;
using System.Collections.Generic;
using System.Text;
using DisqueEngine.Raytracer.Worlds;
using DisqueEngine.Math;

namespace DisqueEngine.Raytracer.Tracers
{
    public class RayCast : Tracer
    {
        public RayCast(World world)
            : base(world)
        {
        }

        public override Vector3 TraceRay(Ray ray)
        {
            ShadeRec sr = World.HitObjects(ray);
            if (sr.Hit_an_object)
            {
                sr.Ray = ray;
                return (sr.Material.Shade(sr));
            }
            else
                return (World.Background);
        }

        public override Vector3 TraceRay(Ray ray, int depth)
        {
            return TraceRay(ray);
        }
    }
}
