using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace WebPage.Implementation
{
    public class LinearRegressionResult
    {
        public int Degree { get; private set; }
        public Vector<double> X_train { get { return this.givenData.X_train; } }
        public Vector<double> T_train { get { return this.givenData.T_train; } }
        public Vector<double> X_test { get { return this.givenData.X_test; } }
        public Vector<double> T_test { get { return this.calibratedData.T_test; } }
        public Vector<double> T_expect { get { return this.givenData.T_expect; } }
        public Vector<double> Weights { get { return this.calibratedData.Weights; } }

        public List<string> Str_X_train { get { return ToStrings(this.X_train); } }
        public List<string> Str_T_train { get { return ToStrings(this.T_train); } }
        public List<string> Str_X_test { get { return ToStrings(this.X_test); } }
        public List<string> Str_T_test { get { return ToStrings(this.T_test); } }
        public List<string> Str_T_expect { get { return ToStrings(this.T_expect); } }
        public List<string> Str_Weights { get { return ToStrings(this.Weights); } }

        private static List<string> ToStrings(Vector<double> vec)
        {
            return vec.Select(v => v.ToString()).ToList();
        }

        private GivenData givenData;
        private CalibratedData calibratedData;

        public LinearRegressionResult(
            int degree,
            GivenData givenData,
            CalibratedData calibratedData)
        {
            this.Degree = degree;
            this.givenData = givenData;
            this.calibratedData = calibratedData;
        }

        public class GivenData
        {
            public readonly Vector<double> X_train;
            public readonly Vector<double> T_train;
            public readonly Vector<double> X_test;
            public readonly Vector<double> T_expect;

            public GivenData(
                Vector<double> x_train,
                Vector<double> t_train,
                Vector<double> x_test,
                Vector<double> t_expect)
            {
                this.X_train = x_train;
                this.T_train = t_train;
                this.X_test = x_test;
                this.T_expect = t_expect;
            }
        }
        public class CalibratedData
        {
            public readonly Vector<double> T_test;
            public readonly Vector<double> Weights;

            public CalibratedData(Vector<double> t_test, Vector<double> weights)
            {
                this.T_test = t_test;
                this.Weights = weights;
            }
        }
    }
}
