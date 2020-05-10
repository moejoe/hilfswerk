using System;
using System.Linq;
using System.Linq.Expressions;

namespace Hilfswerk.EntityFramework.Stores
{
    internal static class Projector
    {
        public static Expression<System.Func<Entities.Einsatz, Models.Einsatz>> EinsatzProjection =>
            x => new Models.Einsatz
            {
                Id = x.Id,
                Anmerkungen = x.Anmerkungen,
                Hilfesuchender = x.Hilfesuchender,
                Taetigkeit = TaetigkeitFromId(x.TaetigkeitId),
                VermitteltAm = new DateTimeOffset(x.VermitteltAm, TimeSpan.Zero),
                Dauer = x.Dauer,
                VermitteltDurch = x.VermitteltDurch,
                Helfer = new Models.Helfer
                {
                    Id = x.Helfer.Id
                }
            };

        public static Expression<Func<int, Models.Taetigkeit>> TaetigkeitProjection =>
            x => x == Entities.Taetigkeit.GASSI_GEHEN.Id ? Models.Taetigkeit.GASSI_GEHEN :
                    x == Entities.Taetigkeit.BESORGUNG.Id ? Models.Taetigkeit.BESORGUNG :
                    x == Entities.Taetigkeit.TELEFON_KONTAKT.Id ? Models.Taetigkeit.TELEFON_KONTAKT :
                        Models.Taetigkeit.ANDERE;

        public static Expression<Func<Entities.Helfer, Models.Helfer>> HelferProjection =>
            x => new Models.Helfer
            {
                Id = x.Id,
                Anmerkung = x.Anmerkung,
                istRisikogruppe = x.istRisikogrupepe,
                hatAuto = x.hatAuto,
                istZivildiener = x.istZivildiener,
                istFreiwilliger = x.istFreiwilliger,
                istAusgelastet = x.istAusgelastet,
                istDSGVOKonform = x.istDSGVOKonform,
                Kontakt = new Models.Kontakt
                {
                    Email = x.Kontakt.Email,
                    GeoLocation = x.Kontakt.GeoLocation,
                    Nachname = x.Kontakt.Nachname,
                    Plz = x.Kontakt.Plz,
                    Strasse = x.Kontakt.Strasse,
                    Telefon = x.Kontakt.Telefon,
                    Vorname = x.Kontakt.Vorname
                },
                Einsaetze = x.Einsaetze.Select(d => new Models.Einsatz
                {
                    Id = d.Id,
                    Anmerkungen = d.Anmerkungen,
                    Hilfesuchender = d.Hilfesuchender,
                    Taetigkeit = TaetigkeitFromId(d.TaetigkeitId),
                    VermitteltAm = new DateTimeOffset(d.VermitteltAm, TimeSpan.Zero),
                    VermitteltDurch = d.VermitteltDurch,
                    Dauer = d.Dauer,
                    Helfer = new Models.Helfer
                    {
                        Id = d.Helfer.Id
                    }
                })
            };

        public static Func<int, Models.Taetigkeit> CompiledTaetigkeitProjection = TaetigkeitProjection.Compile();
        public static Models.Taetigkeit TaetigkeitFromId(int id)
        {
            return CompiledTaetigkeitProjection.Invoke(id);
        }
    }
}
