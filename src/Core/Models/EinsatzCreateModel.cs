namespace Hilfswerk.Models
{
    public class EinsatzCreateModel
    {
        public string HelferId { get; set; }
        public Taetigkeit Taetigkeit { get; set; }
        public string Hilfesuchender { get; set; }
        public string Anmerkungen { get; set; }
        public string VermitteltDurch { get; set; }
    }
}
