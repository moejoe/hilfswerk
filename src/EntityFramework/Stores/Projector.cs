﻿using System.Linq.Expressions;

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
                Kontakt = new Models.Kontakt
                {
                    Email = x.Kontakt.Email,
                    GeoLocation = x.Kontakt.GeoLocation,
                    Nachname = x.Kontakt.Nachname,
                    Plz = x.Kontakt.Plz,
                    Strasse = x.Kontakt.Strasse,
                    Telefon = x.Kontakt.Telefon,
                    Vorname = x.Kontakt.Vorname
                }
            };

        public static Models.Taetigkeit TaetigkeitFromId(int id)
        {
            return TaetigkeitProjection.Compile().Invoke(id);
        }
    }
}