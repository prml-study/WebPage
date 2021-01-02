using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace WebPage.Implementation
{
    public static class Fig_1_2
    {
        public class Restriction
        {
            public readonly bool IsValid;
            public readonly string ErrorMessage;

            public Restriction(bool isValid, string errorMessage)
            {
                this.IsValid = isValid;
                this.ErrorMessage = errorMessage;
            }
        }
        public static Restriction Check(int polynomialDegree, int noiseSeed)
        {
            bool isValid = false;
            string errorMessage = "";
            
            if (0 <= polynomialDegree && polynomialDegree <= 10)
                isValid = true;
            else
            {
                isValid = false;
                errorMessage += $"0 <= polynomialDegree <=10 should be satisfied. ";
            }

            if (noiseSeed <= 0)
            {
                isValid = false;
                errorMessage += $"0 < noiseSeed should be satisfied. ";
            }

            return new Restriction(isValid, errorMessage);
        }
        public static LinearRegressionResult.GivenData GenerateGivenData(int noiseSeed)
        {
            //prepare train data
            Vector<double> x_train = Fig_1_2.X_train;
            Vector<double> t_train = GenerateToyData(noiseSeed, x_train);
            //prepate test data
            Vector<double> x_test = Fig_1_2.X_test;
            Vector<double> t_expect = Vector<double>.Build.DenseOfEnumerable(x_test.Select(x => Fig_1_2_func(x)));

            return new LinearRegressionResult.GivenData(x_train, t_train, x_test, t_expect);
        }

        public static LinearRegressionResult Run(int polynomialDegree, int noiseSeed)
        {
            LinearRegressionResult.GivenData data = GenerateGivenData(noiseSeed);
            (int, LinearRegressionResult.CalibratedData) tpl = Predict(data, polynomialDegree);

            return new LinearRegressionResult(
                degree: tpl.Item1,
                givenData: data,
                calibratedData: tpl.Item2);
        }
        public static IEnumerable<LinearRegressionResult> Run()
        {
            LinearRegressionResult.GivenData data = GenerateGivenData(Fig_1_2.NoiseSeed);
            return Run(data);
        }

        private static IEnumerable<LinearRegressionResult> Run(LinearRegressionResult.GivenData data)
        {
            return PolynomialDegrees
                .Select(polynomialDegree => Predict(data, polynomialDegree))
                .Select(tpl => new LinearRegressionResult(
                    degree: tpl.Item1,
                    givenData: data,
                    calibratedData: tpl.Item2));
        }
        private static (int, LinearRegressionResult.CalibratedData) Predict(LinearRegressionResult.GivenData data, int polynomialDegree)
        {
            IFeature feature = new PolynomialFeature(polynomialDegree);
            Matrix<double> designMatrix = ToyDataGenerater.DesignMatrix(data.X_train, feature);
            Matrix<double> invDesignMatrix = designMatrix.PseudoInverse();
            Vector<double> weights = invDesignMatrix * data.T_train;
            Vector<double> t_test = Predicts(data.X_test, weights, feature);

            return (polynomialDegree, new LinearRegressionResult.CalibratedData(t_test, weights));
        }
        
        public static Vector<double> GenerateToyData(int seed, Vector<double> x_train)
        {
            var generater = RndGenerater.Build(seed);
            Func<double> noise = () => generater.NextNormal(NoiseNormalMean, NoiseNormalStddev);
            return ToyDataGenerater.Generate2D(Fig_1_2_func, noise, x_train);
        }
        public static Vector<double> Predicts(Vector<double> x_test, Vector<double> weights, IFeature feature)
        {
            return Vector<double>.Build.DenseOfEnumerable(x_test.Select(x => feature.Predict(x, weights)));
        }

        public static Vector<double> X_train
        {
            get
            {
                return Xs(10);
            }
        }
        public static Vector<double> X_test
        {
            get
            {
                return Xs(100);
            }
        }
        public static Func<double, double> Fig_1_2_func
        {
            get
            {
                return x => Math.Sin(2.0 * Math.PI * x);
            }
        }
        public static int NoiseSeed
        {
            get
            {
                return 1;
            }
        }
        public static double NoiseNormalMean
        {
            get
            {
                return 0.0;
            }
        }
        public static double NoiseNormalStddev
        {
            get
            {
                return 1.0;
            }
        }
        public static IEnumerable<int> PolynomialDegrees
        {
            get
            {
                return new[] { 0, 1, 3, 9, };
            }
        }

        private static Vector<double> Xs(int cnt)
        {
            return Vector<double>.Build.DenseOfEnumerable(Enumerable.Range(0, cnt).Select(i => i * 1.0 / cnt));
        }

    }
}