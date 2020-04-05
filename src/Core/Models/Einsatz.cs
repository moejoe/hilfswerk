﻿using System;

namespace Hilfswerk.Models
{
    public class Einsatz
    {
        public string Id { get; set; }
        public Helfer Helfer { get; set; }
        public Taetigkeit? Taetigkeit { get; set; }
        public string Hilfesuchender { get; set; }
        public string VermitteltDurch { get; set; }
        public DateTime VermitteltAm { get; set; }
        public string Anmerkungen { get; set; }
        public int? Stunden { get; set; }
    }
}
