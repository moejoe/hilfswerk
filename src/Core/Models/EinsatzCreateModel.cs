﻿namespace Hilfswerk.Models
{
    public class EinsatzCreateModel
    {
        public Taetigkeit Taetigkeit { get; set; }
        public string Hilfesuchender { get; set; }
        public string Anmerkungen { get; set; }
        public string VermitteltDurch { get; set; }
    }
}
