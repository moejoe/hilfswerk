using System;
using System.Linq;

namespace Hilfswerk.Core.Models
{

    public class EinsatzCreateModel
    {
        public string HelferId { get; set; }
        public Taetigkeit Taetigkeit { get; set; }
        public string Hilfesuchender { get; set; }
        public string Anmerkungen { get; set; }
        public string VermitteltDurch { get; set; }
    }

    public class HelferCreateModel
    {
        public Kontakt Kontakt { get; set; }
        public string Anmerkung { get; set; }
        public Taetigkeit[] Taetigkeiten { get; set; }
        public bool hatAuto { get; set; }
        public bool istRisikogruppe { get; set; }
    }

    public static class HelferMappings
    {
        public static EntityFramework.Entities.Helfer ToEntity(this HelferCreateModel createModel, string id)
        {
            return new EntityFramework.Entities.Helfer
            {
                Id = id,
                Anmerkung = createModel.Anmerkung,
                hatAuto = createModel.hatAuto,
                HelferTaetigkeiten = createModel.Taetigkeiten.Select(p => new EntityFramework.Entities.HelferTaetigkeit { TaetigkeitId = (int)p }).ToArray(),
                istRisikogrupepe = createModel.istRisikogruppe,
                Kontakt = createModel.Kontakt.ToEntity()
            };
        }

        public static EntityFramework.Entities.Kontakt ToEntity(this Kontakt model)
        {
            return new EntityFramework.Entities.Kontakt
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

        public static void ApplyTo(this EinsatzCreateModel model, EntityFramework.Entities.Einsatz entity)
        {
            entity.Anmerkungen = model.Anmerkungen;
            entity.TaetigkeitId = (int)model.Taetigkeit;
            entity.Hilfesuchender = model.Hilfesuchender;
        }
    }


    public class Helfer
    {
        public string Id { get; set; }
        public Kontakt Kontakt { get; set; }
        public string Anmerkung { get; set; }
        public Taetigkeit Taetigkeiten { get; set; }
        public Einsatz[] Einsaetze { get; set; } = Array.Empty<Einsatz>();
        public bool hatAuto { get; set; }

    }
}
