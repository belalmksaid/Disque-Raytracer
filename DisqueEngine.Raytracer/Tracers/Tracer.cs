using System;
using System.Collections.Generic;
using System.Text;
using DisqueEngine.Raytracer.Worlds;
using DisqueEngine.Math;

namespace DisqueEngine.Raytracer.Tracers
{
    public class Tracer
    {
        public World World { get; set; }
        public Tracer(World world)
        {
            World = world;
        }
        public virtual Vector3 TraceRay(Ray ray)
        {
            return new Vector3(0, 0, 0);
        }
        public virtual Vector3 TraceRay(Ray ray, int depth)
        {
            return TraceRay(ray);
        }
        public virtual Vector3 TraceRay(Ray ray, ref float tmin, int depth)
        {
            return new Vector3(0, 0, 0);
        }
    }
}
