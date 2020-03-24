namespace Hilfswerk.EntityFramework.Entities
{
    public class HelferTaetigkeit
    {
        public static HelferTaetigkeit BESORGUNG => new HelferTaetigkeit { TaetigkeitId = Taetigkeit.BESORGUNG.Id };
        public static HelferTaetigkeit TELEFON_KONTAKT => new HelferTaetigkeit { TaetigkeitId = Taetigkeit.TELEFON_KONTAKT.Id };
        public static HelferTaetigkeit GASSI_GEHEN => new HelferTaetigkeit { TaetigkeitId = Taetigkeit.GASSI_GEHEN.Id };
        public static HelferTaetigkeit ANDERE => new HelferTaetigkeit { TaetigkeitId = Taetigkeit.ANDERE.Id };

        public string HelferId { get; set; }
        public Helfer Helfer { get; set; }

        public int TaetigkeitId { get; set; }
        public Taetigkeit Taetigkeit { get; set; }
    }

}