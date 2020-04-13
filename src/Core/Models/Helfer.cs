using System;
using System.Collections.Generic;

namespace Hilfswerk.Models
{
    public class Helfer
    {
        public string Id { get; set; }
        public Kontakt Kontakt { get; set; }
        public string Anmerkung { get; set; }
        public Taetigkeit Taetigkeiten { get; set; }
        public IEnumerable<Einsatz> Einsaetze { get; set; } = Array.Empty<Einsatz>();
        public bool hatAuto { get; set; }
        public bool istRisikogruppe { get; set; }
        public bool istZivildiener { get; set; }
        public bool istFreiwilliger { get; set; }
        public bool istAusgelastet { get; set; }
        public bool istDSGVOKonform { get; set; }

    }
}
