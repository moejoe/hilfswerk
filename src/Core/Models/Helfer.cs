using System;
using System.Linq;

namespace Hilfswerk.Models
{

    


    public class Helfer
    {
        public string Id { get; set; }
        public Kontakt Kontakt { get; set; }
        public string Anmerkung { get; set; }
        public Taetigkeit Taetigkeiten { get; set; }
        public Einsatz[] Einsaetze { get; set; } = Array.Empty<Einsatz>();
        public bool hatAuto { get; set; }
        public bool istRisikogruppe { get; set; }
        public bool istZivildiener { get; set; }
        public bool istFreiwilliger { get; set; }

    }
}
