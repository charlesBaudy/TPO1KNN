using System;
using System.Collections.Generic;
using TPO1KNN.model;

namespace TPO1KNN
{
    class MainClass
    {
        public static void Main(string[] args)
        {

            // Console.WriteLine("Hello World!");

            KNN knn = new KNN();

            List<float> distances = new List<float>();

            float distance = 0.0F;

            List<Diagnostic> diagnostics =new List<Diagnostic>();

            List<bool> labels = new List<bool>();

            diagnostics = knn.ImportSamples("samples.csv");

            int n = diagnostics.Count;
            //Console.WriteLine(n);
            int i = 0;

            Diagnostic diagnostic = new Diagnostic();

            diagnostic.Cp = 3.0F;
            diagnostic.Ca = 2.0F;
            diagnostic.Oldpeak = 0.8F;
            diagnostic.Thal = 2.0F;

            for (i = 0; i < n; i++)
            {
                distance = knn.EuclideanDistance(diagnostic, diagnostics[i]);
                distances.Add(distance);
                labels.Add(diagnostics[i].Label);
                //Console.WriteLine(distance+" -> "+ diagnostics[i].Label);
            }

            //Console.WriteLine("-------------------------");

            knn.ShellSort(distances, labels);

            for (i = 0; i < n; i++)
            {
                Console.WriteLine(distances[i] + " -> " + labels[i]);
            }

            bool verdict = knn.Vote(labels);

            Console.WriteLine("verdict = "+verdict);
        }
    }
}
