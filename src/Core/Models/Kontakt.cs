namespace Hilfswerk.Models
{
    public class Kontakt
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int Plz { get; set; }
        public string Strasse { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string GeoLocation { get; set; }
    }
}
