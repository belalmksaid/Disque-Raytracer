using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisqueEngine.Raytracer.Worlds;

namespace DisqueEngine.Raytracer.Tracers
{
    public class PhotonTracer : PathTrace
    {
        public PhotonTracer(World world)
            : base(world)
        {
        }
    }
}