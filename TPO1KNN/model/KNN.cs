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
            float cp = (first_sample.Cp - second_sample.Cp) * (first_sample.Cp - second_sample.Cp);
            float ca = (first_sample.Ca - second_sample.Ca) * (first_sample.Ca - second_sample.Ca);
            float thal = (first_sample.Thal - second_sample.Thal) * (first_sample.Thal - second_sample.Thal);
            float oldPeak = (first_sample.Oldpeak - second_sample.Oldpeak) * (first_sample.Oldpeak - second_sample.Oldpeak);

            return (float)Math.Sqrt(cp + ca + thal + oldPeak);

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
            float cp = Math.Abs(first_sample.Cp - second_sample.Cp);
            float ca = Math.Abs(first_sample.Ca - second_sample.Ca);
            float thal = Math.Abs(first_sample.Thal - second_sample.Thal);
            float oldPeak = Math.Abs(first_sample.Oldpeak - second_sample.Oldpeak);

            return cp + ca + thal + oldPeak;
        }

        public bool Predict(Diagnostic sample_to_predict)
        {
            throw new NotImplementedException();
        }

        public void ShellSort(List<float> distances, List<bool> labels)
        {
            int n = distances.Count-1;
            int h = 1;
            int i = 0;
            int j = 0;

            while (h < n/3)
            {
                h = 3 * h + 1;
            }

            while (h >= 1)
            {
                for (i = h + 1; i <= n; i++)
                {
                    float tmp = distances[i];
                    bool tmpLabel = labels[i];
                    j = i;
                    while(j-h>=1 && distances[j - h] > tmp)
                    {
                        labels[j] = labels[j - h];
                        distances[j] = distances[j - h];
                        j = j - h;
                    }
                    labels[j] = tmpLabel;
                    distances[j] = tmp;
                }
                h = h / 3;
            }
        }

        public void Train(string filename_train_samples_csv, int k = 1, int distance = 1)
        {
            List<Diagnostic> diagnostics = this.ImportSamples(filename_train_samples_csv);

            int n = diagnostics.Count;

            Diagnostic diagnostic = new Diagnostic();

            for(int x=0;x<n;x++)
                for(int y=0;y<n;y++)
                    for (int z = 0; z < n; z++)
                        for (int t = 0; t < n; t++)
                        {
                            diagnostics[x].Cp = x*10;
                            diagnostics[x].Ca = y*10;
                            diagnostics[x].Oldpeak = z*10;
                            diagnostics[x].Thal = t*10;
                            EuclideanDistance(first_sample,);
                        }


        }

        public bool Vote(List<bool> sorted_labels)
        {
            int k = 1;
            int cptPositive = 0;
            int cptNegative = 0;

            for (int i = 0; i <= k; i++)
            {
                if (sorted_labels[i] == true)
                {
                    cptPositive++;
                } else
                {
                    cptNegative++;
                }
            }
            if(cptNegative>cptPositive)
            {
                return false;
            } else if (cptNegative < cptPositive)
            {
                return true;
            } else
            {
                return true;
            }
        }
    }
}
