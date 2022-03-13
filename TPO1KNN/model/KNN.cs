using System;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Globalization;
using System.Linq;

namespace TPO1KNN.model
{
    public class KNN : TP01KNN.model.IKNN
    {
        public KNN()
        {

        }

        public float EuclideanDistance(Diagnostic first_sample, Diagnostic second_sample)
        {
            throw new NotImplementedException();
        }

        public float Evaluate(string filename_test_samples_csv)
        {
            throw new NotImplementedException();
        }

        public List<Diagnostic> ImportSamples(string filename_samples_csv)
        {
            List<Diagnostic> diagnostics = new List<Diagnostic>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            using (var reader = new StreamReader("/Users/charlesbaudy/Projects/TPO1KNN/TPO1KNN/data/"+filename_samples_csv))
            using (var csv = new CsvReader(reader, config))
            {
                diagnostics = csv.GetRecords<Diagnostic>().ToList();
            }

            return diagnostics;
        }

        public float ManhattanDistance(Diagnostic first_sample, Diagnostic second_sample)
        {
            throw new NotImplementedException();
        }

        public bool Predict(Diagnostic sample_to_predict)
        {
            throw new NotImplementedException();
        }

        public void ShellSort(List<float> distances, List<bool> labels)
        {
            throw new NotImplementedException();
        }

        public void Train(string filename_train_samples_csv, int k = 1, int distance = 1)
        {
            throw new NotImplementedException();
        }

        public bool Vote(List<bool> sorted_labels)
        {
            throw new NotImplementedException();
        }
    }
}
