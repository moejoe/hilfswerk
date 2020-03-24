using System;
using System.Collections.Generic;

namespace Hilfswerk.EntityFramework.Entities
{
    public class Taetigkeit
    {
        public static Taetigkeit BESORGUNG => new Taetigkeit { Id = 1, Label = "Besorgung" };
        public static Taetigkeit TELEFON_KONTAKT => new Taetigkeit { Id = 2, Label = "Telefonkontakt" };
        public static Taetigkeit GASSI_GEHEN => new Taetigkeit { Id = 4, Label = "Gassi gehen" };
        public static Taetigkeit ANDERE => new Taetigkeit { Id = 8, Label = "Andere" };

            
        public int Id { get; set; }
        public string Label { get; set; }
        public ICollection<HelferTaetigkeit> HelferTaetigkeiten { get; set; }
        public ICollection<Einsatz> Einsaetze { get; set; }
    }
}
