using Hilfswerk.Core.Models;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TestData
    {
        public static Helfer[] Helfer => new[]
            {
                new Helfer
                {
                    Id = "ca277ca2-d1fe-4b99-af90-ef50b86cb3b1",
                    Kontakt = new Kontakt
                    {
                        Vorname = "Zoe",
                        Nachname = "Miller",
                        Email = "zoe.miller@noreply.com",
                        Plz = 1170,
                        Strasse = "Von-Kronberg-Straße",
                        Telefon = "+43 650 1234567"
                    },
                    Anmerkung = "Ist mit EBE verwandt",
                    Taetigkeiten = new []
                    {
                        Taetigkeit.TELEFON_KONTAKT
                    },
                },
                new Helfer
                {
                    Id = "38ac685f-c3b9-4b67-9a19-5402a7a96f79",
                    Kontakt = new Kontakt
                    {
                        Vorname = "Veerle",
                        Nachname = "de Bree",
                        Email = "veerle.deBree@noreply.com",
                        Plz = 1170,
                        Strasse = "Karl-Milllöcker-Straße",
                        Telefon = "+43 650 1234567"
                    },
                    Anmerkung = "???",
                    Taetigkeiten = new []
                    {
                        Taetigkeit.BESORGUNG,
                        Taetigkeit.TELEFON_KONTAKT
                    },
                },
                new Helfer
                {
                    Id = "da522c4c-fbaa-4c7d-884c-00f605943f6d",
                    Kontakt = new Kontakt
                    {
                        Vorname = "Udom",
                        Nachname = "Paowsong",
                        Email = "udom.paowsong@noreply.com",
                        Plz = 1170,
                        Strasse = "Auf dem Göbler",
                        Telefon = "+43 650 1234567"
                    },
                    Anmerkung = "Kann Deutsch, Enlisch, Kroatisch"
                },
                new Helfer
                {
                    Id = "6cd27a95-f769-423c-bb77-897a2ced93f7",
                    Kontakt = new Kontakt
                    {
                        Vorname = "Thomas",
                        Nachname = "Bunce",
                        Email = "thomas.bunce@noreply.com",
                        Plz = 1170,
                        Strasse = "Ledererstraße",
                        Telefon = "+43 650 1234567"
                    },
                    Anmerkung = "Wirkt eher verdächtig",
                    Taetigkeiten = new []
                    {
                        Taetigkeit.BESORGUNG,
                        Taetigkeit.TELEFON_KONTAKT
                    },
                },
                new Helfer
                {
                    Id ="c5edf6a5-fb9b-4c1a-9e1a-2460cf946fe9",
                    Kontakt = new Kontakt
                    {
                        Vorname = "Sara",
                        Nachname = "Scholz",
                        Email = "sara.scholz@noreply.com",
                        Plz = 1170,
                        Strasse = "Paartalstraße",
                        Telefon = "+43 650 1234567"
                    },
                    Anmerkung = "Lorem ipsum dolor sit amet",
                    Taetigkeiten = new []
                    {
                        Taetigkeit.GASSI_GEHEN,
                        Taetigkeit.TELEFON_KONTAKT
                    },
                },
                new Helfer
                {
                    Id = "eaa43067-3524-402a-bb97-5d9f5db11cb0",
                    Kontakt = new Kontakt
                    {
                        Vorname = "Santiago",
                        Nachname = "Valentin",
                        Email = "santiago.valentin@noreply.com",
                        Plz = 1170,
                        Strasse = "Ahr-Rotweinstraße",
                        Telefon = "+43 650 1234567"
                    },
                    Anmerkung = "Dringender Rückruf",
                    Taetigkeiten = new []
                    {
                        Taetigkeit.BESORGUNG,
                        Taetigkeit.GASSI_GEHEN
                    },
                },
                new Helfer
                {
                    Id = "e3728bf2-6473-4766-b73f-99343fff6d9b",
                    Kontakt = new Kontakt
                    {
                        Vorname = "Rickie",
                        Nachname = "Baroch",
                        Email = "rickie.baroch@noreply.com",
                        Plz = 1170,
                        Strasse = "Zum Seegraben",
                        Telefon = "+43 650 1234567"
                    },
                    Taetigkeiten = new []
                    {
                        Taetigkeit.GASSI_GEHEN,
                        Taetigkeit.TELEFON_KONTAKT
                    },
                    Anmerkung = "Hat bereits bei 2 Einsätzen Probleme gehabt"
                },
                new Helfer
                {
                    Id = "1de32347-ac62-4f57-bcc7-9eea9f898884",
                    Kontakt = new Kontakt
                    {
                        Vorname = "Moritz",
                        Nachname = "Haslhofer",
                        Email = "moritz.haslhofer@noreply.com",
                        Plz = 1020,
                        Strasse = "Volkertplatz",
                        Telefon = "+43 650 1234567"
                    },
                    Anmerkung = "Ist Mittags nicht erreichbar.",
                    hatAuto = false,
                    Taetigkeiten = new []
                    {
                        Taetigkeit.BESORGUNG,
                        Taetigkeit.GASSI_GEHEN,
                        Taetigkeit.TELEFON_KONTAKT
                    },
                    Einsaetze = new Einsatz[]
                    {
                        new Einsatz
                        {
                            Anmerkungen = "keine Anmerkung",
                            Hilfesuchender = "Anna Alisg",
                            VermitteltAm = DateTime.Parse("2020-03-22 14:17:00"),
                            Taetigkeit = Taetigkeit.BESORGUNG,
                            VermitteltDurch = "Martha Mitarbeiterin"
                        },
                        new Einsatz
                        {
                            Anmerkungen = "keine Anmerkung",
                            Hilfesuchender = "Anna Alisg",
                            VermitteltAm = DateTime.Parse("2020-03-21 16:17:00"),
                            Taetigkeit = Taetigkeit.BESORGUNG,
                            VermitteltDurch = "Martha Mitarbeiterin"
                        }
                        ,
                        new Einsatz
                        {
                            Anmerkungen = "keine Anmerkung",
                            Hilfesuchender = "Anna Alisg",
                            VermitteltAm = DateTime.Parse("2020-03-20 16:17:00"),
                            Taetigkeit = Taetigkeit.BESORGUNG,
                            VermitteltDurch = "Martha Mitarbeiterin"
                        }
                    }
                }
            };
    }
}
