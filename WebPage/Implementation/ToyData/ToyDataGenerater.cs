using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace WebPage.Implementation
{
    public static class ToyDataGenerater
    {
        public static Vector<double> Generate2D(
            Func<double, double> func,
            Func<double> noise,
            Vector<double> xs)
        {
            return Vector<double>.Build.DenseOfEnumerable(xs.Select(x => func(x) + noise()));
        }

        public static Matrix<double> DesignMatrix(Vector<double> xs, IFeature feature)
        {
            return Matrix<double>.Build.DenseOfRowVectors(xs.Select(x => feature.DesignVector(x)));
        }
    }
}
