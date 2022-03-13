using System;
using CsvHelper.Configuration.Attributes;

namespace TPO1KNN.model
{
    public class Diagnostic : TP01KNN.model.IDiagnostic
    {
        [Name("cp")]
        public float Cp { get; set; }

        [Name("ca")]
        public float Ca { get; set; }

        [Name("thal")]
        public float Thal { get; set; }

        [Name("oldpeak")]
        public float Oldpeak { get; set; }

        public float[] Features { get; }

        [Name("target")]
        public bool Label { get; set; }

        public Diagnostic()
        {

        }

        public void PrintInfo()
        {
            Console.Write("cp : "+Cp+" target : "+Label+"\n");
        }

        /*public void SetLabel(int data) {
            if(data == 1) { this.Label = true}
        }*/
    }
}
