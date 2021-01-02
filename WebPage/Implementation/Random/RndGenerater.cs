using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace WebPage.Implementation
{
    public class RndGenerater
    {
        public readonly int Seed;
        public readonly IUniform Uniform;

        public static RndGenerater Build(int seed)
        {
            IUniform uniform = UniformMersenneTwister.Build(seed);
            return new RndGenerater(seed, uniform);
        }
        private RndGenerater(int seed, IUniform uniform)
        {
            this.Seed = seed;
            this.Uniform = uniform;
        }

        public static double NextNormal(double mean, double stddev, double uniform)
        {
            return Normal.InvCDF(mean, stddev, uniform);
        }
        public double NextNormal(double mean, double stddev)
        {
            double uniform = this.Uniform.NextDouble();
            return NextNormal(mean, stddev, uniform);
        }
        public IEnumerable<double> NextNormals(double mean, double stddev, int size)
        {
            return Enumerable.Range(0, size).Select(i => NextNormal(mean, stddev));
        }
    }
}
