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

            List<Diagnostic> diagnostics =new List<Diagnostic>();

            diagnostics = knn.ImportSamples("samples.csv");

            foreach(Diagnostic item in diagnostics)
            {
                item.PrintInfo();
            }
        }
    }
}
