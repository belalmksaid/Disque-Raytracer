using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisqueEngine.Raytracer;
using DisqueEngine.Math;
using DisqueEngine.Raytracer.Worlds;
using DisqueEngine.Raytracer.Lights;
using DisqueEngine.Raytracer.Cameras;
using DisqueEngine.Raytracer.Tracers;
using DisqueEngine.Raytracer.Samplers;

namespace AlgorithmTester
{
    class Program
    {
        static World world2;
        static void Main(string[] args)
        {

            Console.ReadKey();            
        }
        void build10()
        {
            World world = new World();
            int ns = 49;
            world.ViewPlane.HRes = 400;
            world.ViewPlane.VRes = 400;
            world.ViewPlane.SetSampler(new Regular(ns));
            world.ViewPlane.MaxDepth = 10;
            world.Tracer = new Whitted(world);
            AmbientOccluder occ = new AmbientOccluder();
            occ.RadianceScale = 1.0f;
            occ.MinAmount = 0f;
            occ.Shadows = true;
            occ.Color = Vector3.One();
            occ.SetSampler(new MultiJittered(ns));
            world.AmbientLight = occ;
            Pinhole cam = new Pinhole();
            cam.Position = new Vector3(40, 30, 20);
            cam.Target = new Vector3(0, 0, 0);
            cam.Distance = 5500;
            cam.Zoom = 1.5f;
            world.Camera = cam;
            PointLight pl = new PointLight();
            pl.Color = Vector3.One();
            pl.Position = new Vector3(4, 4, 0);
            pl.RadianceScale = 1.0f;
            pl.Shadows = true;
            world.Lights.Add(pl);
        }

    }
    public class Dog
    {
        public string name;
        public Dog(string n)
        {
            name = n;
        }

        public static void Foo(ref Dog d)
        {
            d.name = "Baloo";
            d = new Dog("Flo");
        }
    }
}
