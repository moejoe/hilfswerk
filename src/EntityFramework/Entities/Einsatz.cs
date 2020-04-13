using System;

namespace Hilfswerk.EntityFramework.Entities
{
    public class Einsatz
    {
        public string Id { get; set; }
        public Helfer Helfer { get; set; }
        public Taetigkeit Taetigkeit { get; set; }
        public int TaetigkeitId { get; set; }
        public string Hilfesuchender { get; set; }
        public string VermitteltDurch { get; set; }
        public DateTime VermitteltAm { get; set; }
        public string Anmerkungen { get; set; }
        public int? Stunden { get; set; }
        public TimeSpan? Dauer { get; set; }

    }

    public class Hilfesuchender
    {
        public string Id { get; set; }
        public Kontakt Kontakt { get; set; }
    }
}
