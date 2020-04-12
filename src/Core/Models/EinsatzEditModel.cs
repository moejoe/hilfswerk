using System;

namespace Hilfswerk.Models
{
    public class EinsatzEditModel
    {
        public string Anmerkungen { get; set; }
        public DateTimeOffset VermitteltAm { get; set; }
        public int? Stunden { get; set; }
    }
}
