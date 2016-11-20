using System;
using System.Collections.Generic;
using System.Text;
using DisqueEngine.Math;

namespace DisqueEngine.Raytracer.Samplers
{
    public class Regular : Sampler
    {
        public Regular(int n)
            : base(n)
        {
            GenerateSamples();
        }

        public override void GenerateSamples()
        {
            int n = (int)MathUtil.Sqrt((float)NumSamples);
            for (int j = 0; j < NumSets; j++)
                for (int p = 0; p < n; p++)
                    for (int q = 0; q < n; q++)
                        samples.Add(new Vector2((((float)q) + 0.5f) / ((float)n), (((float)p) + 0.5f) / ((float)n)));
        }
    }
}
