using GraphQL.Types;
using Hilfswerk.Models;

namespace Hilfswerk.GraphApi
{
    public class KontaktType : ObjectGraphType<Kontakt>
    {
        public KontaktType()
        {
            Name = "Kontakt";
            Field(x => x.Vorname);
            Field(x => x.Nachname);
            Field(x => x.Plz);
            Field(x => x.Strasse);
            Field(x => x.Telefon, nullable: true);
            Field(x => x.Email, nullable: true);
        }
    }
}
