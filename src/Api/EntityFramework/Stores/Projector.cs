using System.Linq.Expressions;

namespace Hilfswerk.EntityFramework.Stores
{
    internal static class Projector
    {
        public static Expression<System.Func<Entities.Einsatz, Models.Einsatz>> EinsatzProjection =>
            x => new Models.Einsatz
            {
                Anmerkungen = x.Anmerkungen,
                Hilfesuchender = x.Hilfesuchender,
                Taetigkeit = TaetigkeitFromId(x.TaetigkeitId),
                VermitteltAm = x.VermitteltAm,
                VermitteltDurch = x.VermitteltDurch,
                Helfer = new Models.Helfer
                {
                    Id = x.Helfer.Id
                }
            };

        public static Expression<System.Func<int, Models.Taetigkeit>> TaetigkeitProjection =>
            x => x == Entities.Taetigkeit.GASSI_GEHEN.Id ? Models.Taetigkeit.GASSI_GEHEN :
                    x == Entities.Taetigkeit.BESORGUNG.Id ? Models.Taetigkeit.BESORGUNG :
                    x == Entities.Taetigkeit.TELEFON_KONTAKT.Id ? Models.Taetigkeit.TELEFON_KONTAKT :
                        Models.Taetigkeit.ANDERE;

        public static Expression<System.Func<Entities.Helfer, Models.Helfer>> HelferProjection =>
            x => new Models.Helfer
            {
                Id = x.Id,
                Anmerkung = x.Anmerkung,
                istRisikogruppe = x.istRisikogrupepe,
                hatAuto = x.hatAuto,
                Kontakt = FromEntity(x.Kontakt)
            };
        public static Expression<System.Func<Entities.Kontakt, Models.Kontakt>> KontaktProjection =>
            x => new Models.Kontakt
            {
                Email = x.Email,
                GeoLocation = x.GeoLocation,
                Nachname = x.Nachname,
                Plz = x.Plz,
                Strasse = x.Strasse,
                Telefon = x.Telefon,
                Vorname = x.Vorname
            };

        public static Models.Taetigkeit TaetigkeitFromId(int id)
        {
            return TaetigkeitProjection.Compile().Invoke(id);
        }

        public static Models.Kontakt FromEntity(Entities.Kontakt entity)
        {
            return KontaktProjection.Compile().Invoke(entity);
        }
    }
}
