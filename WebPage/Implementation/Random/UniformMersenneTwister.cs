using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.Random;

namespace WebPage.Implementation
{
    public class UniformMersenneTwister : IUniform
    {
        public readonly int Seed;
        public readonly bool ThreadSafe;
        public readonly MersenneTwister MersenneTwister;

        public static UniformMersenneTwister Build(int seed, bool threadSafe = true)
        {
            MersenneTwister mersenneTwister = new MersenneTwister(seed, threadSafe);
            return new UniformMersenneTwister(seed, threadSafe, mersenneTwister);
        }
        private UniformMersenneTwister(int seed, bool threadSafe, MersenneTwister mersenneTwister)
        {
            this.Seed = seed;
            this.ThreadSafe = threadSafe;
            this.MersenneTwister = mersenneTwister;
        }

        public double NextDouble()
        {
            return this.MersenneTwister.NextDouble();
        }

        public IEnumerable<double> NextDoubles(int size)
        {
            return this.MersenneTwister.NextDoubles(size);
        }
    }
}
