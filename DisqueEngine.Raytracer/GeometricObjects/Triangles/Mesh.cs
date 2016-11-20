using System;
using System.Collections.Generic;

using System.Text;
using DisqueEngine.Raytracer.GeometricObjects.CompoundObjects;
using DisqueEngine.Math;
using DisqueEngine.Raytracer.Acceleration;

namespace DisqueEngine.Raytracer.GeometricObjects.Triangles
{
    public class Mesh : Compound
    {
        public readonly List<Vector3> Vertices = new List<Vector3>();
        public readonly List<Vector3> Normals = new List<Vector3>();
        public readonly List<float> U = new List<float>(), V = new List<float>();
        public readonly List<List<int>> VertexFaces = new List<List<int>>();

        public Mesh(string name)
            : base(name)
        {
        }
    }
}
