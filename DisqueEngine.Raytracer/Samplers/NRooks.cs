using System;
using System.Collections.Generic;
using System.Text;
using DisqueEngine.Math;

namespace DisqueEngine.Raytracer.Samplers
{
    public class NRooks : Sampler
    {
        public NRooks(int n)
            : base(n)
        {
            GenerateSamples();
        }

        public NRooks(int n, int sets)
            : base(n, sets)
        {
            GenerateSamples();
        }

        public override void GenerateSamples()
        {
            for (int p = 0; p < NumSets; p++)
                for (int j = 0; j < NumSamples; j++)
                {
                    Vector2 sp = new Vector2((j + MathUtil.RandomFloat()) / NumSamples, (j + MathUtil.RandomFloat()) / NumSamples);
                    samples.Add(sp);
                }
            ShuffleXCoordinates();
            ShuffleYCoordinates();
        }
    }
}
