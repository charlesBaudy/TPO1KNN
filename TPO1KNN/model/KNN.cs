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
        public int K;

        public List<bool> getResults(int k, List<Diagnostic> diagnostics, List<Diagnostic> samples)
        {
            int n = diagnostics.Count;

            List<bool> results = new List<bool>();

            List<float> distances = new List<float>();
            List<bool> labels = new List<bool>();

            for (int i = 0; i < n; i++)
            {
                foreach(Diagnostic d in samples)
                {
                    distances.Add(this.ManhattanDistance(diagnostics[i], d));
                    labels.Add(diagnostics[i].Label);
                }

                this.ShellSort(distances, labels);

                results.Add(Vote(labels, k));
            }


            return results;
        }

        

        public void Train(string filename_train_samples_csv, int k = 1, int distance = 1)
        {
            List<Diagnostic> diagnostics = this.ImportSamples(filename_train_samples_csv);

            List<Diagnostic> samples = this.ImportSamples("samples.csv");

            int n = diagnostics.Count;
            int valeurK = 0;
            float seuil = 0.0F;

            List<float> distances = new List<float>();
            List<bool> labels = new List<bool>();
            List<bool> resultLabels = new List<bool>();

            foreach (Diagnostic d in diagnostics)
            {
                labels.Add(d.Label);
            }

            while (k < samples.Count)
            {
                int cpt = 0;

                resultLabels = getResults(k, diagnostics, samples);
                for(int i=0; i<n; i++)
                {
                    if (resultLabels[i] == labels[i])
                    {
                        cpt++;
                    }
                }

                float pourcentage = 100 * (float)cpt / n;
                if (pourcentage > seuil)
                {
                    seuil = pourcentage;
                    valeurK = k;
                    Console.WriteLine("valeur pourcentage " + seuil);
                    Console.WriteLine("valeur de k=" + k);
                }

                k++;
            }

            this.K = valeurK;
        }

        public float Evaluate(string filename_test_samples_csv)
        {
            List<Diagnostic> diagnostics = this.ImportSamples(filename_test_samples_csv);

            List<Diagnostic> samples = this.ImportSamples("samples.csv");

            int n = diagnostics.Count;

            List<bool> labels = new List<bool>();
            List<bool> resultLabels = new List<bool>();

            foreach (Diagnostic d in diagnostics)
            {
                labels.Add(d.Label);
            }

            
                int cpt = 0;

                resultLabels = getResults(K, diagnostics, samples);
                for (int i = 0; i < n; i++)
                {
                    if (resultLabels[i] == labels[i])
                    {
                        cpt++;
                    }
                }

                return  100 * (float)cpt / n;
        }

        public bool Predict(Diagnostic sample_to_predict)
        {
            List<Diagnostic> diagnostics = new List<Diagnostic>();
            List<float> distances = new List<float>();
            this.Train("train.csv");
            diagnostics = this.ImportSamples("samples.csv");
            int n = diagnostics.Count;
            List<bool> labels = new List<bool>();
            for (int i = 0; i < n; i++)
            {
                float distance = this.EuclideanDistance(sample_to_predict, diagnostics[i]);
                distances.Add(distance);
                labels.Add(diagnostics[i].Label);
            }
            this.ShellSort(distances, labels);
            return this.Vote(labels,this.K);
        }



        public float EuclideanDistance(Diagnostic first_sample, Diagnostic second_sample)
        {
            float cp = (first_sample.Cp - second_sample.Cp) * (first_sample.Cp - second_sample.Cp);
            float ca = (first_sample.Ca - second_sample.Ca) * (first_sample.Ca - second_sample.Ca);
            float thal = (first_sample.Thal - second_sample.Thal) * (first_sample.Thal - second_sample.Thal);
            float oldPeak = (first_sample.Oldpeak - second_sample.Oldpeak) * (first_sample.Oldpeak - second_sample.Oldpeak);

            return (float)Math.Sqrt(cp + ca + thal + oldPeak);

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

        public void ShellSort(List<float> distances, List<bool> labels)
        {
            int n = distances.Count;
            int h = 0;
            int i = 0;
            int j = 0;

            while (h < n)
            {
                h = 3 * h + 1;
            }

            while (h !=0)
            {
                h = h / 3;
                for (i = h; i < n; i++)
                {
                    float tmp = distances[i];
                    bool tmpLabel = labels[i];
                    j = i;
                    while(j>h-1 && distances[j - h] > tmp)
                    {
                        labels[j] = labels[j - h];
                        distances[j] = distances[j - h];
                        int a = j - h;
                        //Console.WriteLine("distances[" + j + "] deviens distances[" + a + "] ");
                        j = j - h;
                    }
                    labels[j] = tmpLabel;
                    distances[j] = tmp;
                }
            }
        }

        public bool Vote(List<bool> sorted_labels, int k)
        {
            int cptPositive = 0;
            int cptNegative = 0;

            for (int i = 0; i < k; i++)
            {
                if (sorted_labels[i] == true)
                {
                    cptPositive++;
                } else
                {
                    cptNegative++;
                }
            }

            //Console.WriteLine("negatif : "+cptNegative+" positif : "+cptPositive);

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
