using Hilfswerk.Models;
using System;

namespace Hilfswerk.Core.Models
{

    public class ReportDetail
    {
        public Taetigkeit Taetigkeit { get; set; }
        public int Einsaetze { get; set; }
        public TimeSpan Dauer { get; set; }
        public int Helfer { get; set; }
    }
}
