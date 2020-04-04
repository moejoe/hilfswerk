using System;
using System.Collections.Generic;

namespace Hilfswerk.EntityFramework.Entities
{
    public class Helfer
    {
        public string Id { get; set; }
        public Kontakt Kontakt { get; set; }
        public string Anmerkung { get; set; }
        public ICollection<HelferTaetigkeit> HelferTaetigkeiten { get; set; } = new List<HelferTaetigkeit>();
        public ICollection<Einsatz> Einsaetze { get; set; } = new List<Einsatz>();
        public bool hatAuto { get; set; }
        public bool istRisikogrupepe { get; set; }
        public bool istZivildiener { get; set; }
        public bool istFreiwilliger { get; set; }
        public bool istAusgelastet { get; set; }
    }

}