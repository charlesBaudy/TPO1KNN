using System;
using System.Collections.Generic;
using TPO1KNN.model;

namespace TPO1KNN
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            KNN knn = new KNN();

            Diagnostic diagnostic = new Diagnostic();

            diagnostic.Cp = 0.0F;
            diagnostic.Ca = 1.0F;
            diagnostic.Oldpeak = 1.4F;
            diagnostic.Thal = 3.0F;

            bool result = knn.Predict(diagnostic);

            Console.WriteLine("k optimal : " + knn.K+" predict : "+result);

            float accuracy = knn.Evaluate("test.csv");

            Console.WriteLine("precision = " + accuracy);
        }
    }
}
