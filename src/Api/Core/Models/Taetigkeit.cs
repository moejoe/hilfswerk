using System;

namespace Hilfswerk.Core.Models
{
    [Flags]
    public enum Taetigkeit
    {
        BESORGUNG = 1,
        TELEFON_KONTAKT = 2,
        GASSI_GEHEN = 4,
        ANDERE = 8,
    }
}
