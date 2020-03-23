using System;

namespace Hilfswerk.Core.Models
{



    public class Helfer
    {
        public string Id { get; set; }
        public Kontakt Kontakt { get; set; }
        public string Anmerkung { get; set; }
        public Taetigkeit[] Taetigkeiten { get; set; } = Array.Empty<Taetigkeit>();
        public Einsatz[] Einsaetze { get; set; } = Array.Empty<Einsatz>();
        public bool hatAuto { get; set; }

    }
}
