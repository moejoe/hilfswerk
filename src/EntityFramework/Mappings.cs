using Hilfswerk.Models;
using System.Linq;

namespace Hilfswerk.EntityFramework
{
    public static class HelferMappings
    {
        public static Entities.Helfer ToEntity(this HelferCreateModel createModel)
        {
            return new Entities.Helfer
            {
                Anmerkung = createModel.Anmerkung,
                hatAuto = createModel.hatAuto,
                HelferTaetigkeiten = createModel.Taetigkeiten.Select(p => new Entities.HelferTaetigkeit { TaetigkeitId = (int)p }).ToArray(),
                istRisikogrupepe = createModel.istRisikogruppe,
                istFreiwilliger = createModel.istFreiwilliger,
                istZivildiener = createModel.istZivildiener,
                Kontakt = createModel.Kontakt.ToEntity()
            };
        }

        public static Entities.Kontakt ToEntity(this Kontakt model)
        {
            return new Entities.Kontakt
            {
                Email = model.Email,
                GeoLocation = model.GeoLocation,
                Nachname = model.Nachname,
                Plz = model.Plz,
                Strasse = model.Strasse,
                Telefon = model.Telefon,
                Vorname = model.Vorname
            };
        }

        public static void ApplyTo(this Kontakt model, Entities.Kontakt kontakt)
        {
            kontakt.Email = model.Email;
            kontakt.GeoLocation = model.GeoLocation;
            kontakt.Nachname = model.Nachname;
            kontakt.Plz = model.Plz;
            kontakt.Strasse = model.Strasse;
            kontakt.Telefon = model.Telefon;
            kontakt.Vorname = model.Vorname;
        }

        public static void ApplyTo(this EinsatzCreateModel model, Entities.Einsatz entity)
        {
            entity.Anmerkungen = model.Anmerkungen;
            entity.TaetigkeitId = (int)model.Taetigkeit;
            entity.Hilfesuchender = model.Hilfesuchender;
            entity.VermitteltDurch = model.VermitteltDurch;
            entity.Stunden = model.Stunden;
        }

        public static void ApplyTo(this HelferEditModel model, Entities.Helfer entity)
        {
            entity.Anmerkung = model.Anmerkung;
            entity.hatAuto = model.hatAuto;
            entity.istRisikogrupepe = model.istRisikogruppe;
            entity.istFreiwilliger = model.istFreiwilliger;
            entity.istZivildiener = model.istZivildiener;
            entity.istAusgelastet = model.istAusgelastet;
            model.Kontakt.ApplyTo(entity.Kontakt);
        }
    }
}
