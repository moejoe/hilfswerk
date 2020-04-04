namespace Hilfswerk.Models
{
    public class HelferCreateModel
    {
        public Kontakt Kontakt { get; set; }
        public string Anmerkung { get; set; }
        public Taetigkeit[] Taetigkeiten { get; set; }
        public bool hatAuto { get; set; }
        public bool istRisikogruppe { get; set; }
        public bool istZivildiener { get; set; }
        public bool istFreiwilliger { get; set; }
        public bool istAusgelastet { get; set; }
    }
}
