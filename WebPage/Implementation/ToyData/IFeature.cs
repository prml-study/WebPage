using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace WebPage.Implementation
{
    public interface IFeature
    {
        public Vector<double> DesignVector(double x);
        public double Predict(double x, Vector<double> weight);
    }
}
