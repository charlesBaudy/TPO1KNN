using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPO1KNN.model;

namespace TP01KNN.model
{
    internal interface IKNN
    {
        /* main methods */
        void Train(string filename_train_samples_csv, int k = 1, int distance = 1);
        float Evaluate(string filename_test_samples_csv);
        bool Predict(Diagnostic sample_to_predict);
        /* utils */
        float EuclideanDistance(Diagnostic first_sample, Diagnostic second_sample);
        float ManhattanDistance(Diagnostic first_sample, Diagnostic second_sample);
        bool Vote(List<bool> sorted_labels);
        void ShellSort(List<float> distances, List<bool> labels);
        List<Diagnostic> ImportSamples(string filename_samples_csv);
    }
}