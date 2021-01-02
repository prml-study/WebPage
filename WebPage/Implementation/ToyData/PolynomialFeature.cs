using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace WebPage.Implementation
{
    public class PolynomialFeature : IFeature
    {
        public readonly int Degree;

        public PolynomialFeature(int degree)
        {
            this.Degree = degree;
        }

        public Vector<double> DesignVector(double x)
        {
            return Vector<double>.Build.DenseOfEnumerable(
                Enumerable.Range(0, this.Degree + 1).Select(i => Math.Pow(x, i)));
        }
        public double Predict(double x, Vector<double> weight)
        {
            return weight * DesignVector(x);
        }
    }
}
