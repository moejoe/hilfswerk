using GraphQL.Types;
using Hilfswerk.Models;

namespace Hilfswerk.GraphApi
{
    public class KontaktInputType : InputObjectGraphType<Kontakt>
    {
        public KontaktInputType()
        {
            Name = "KontaktInput";
            Field(x => x.Vorname);
            Field(x => x.Nachname);
            Field(x => x.Plz);
            Field(x => x.Strasse);
            Field(x => x.Telefon);
            Field(x => x.Email);
        }
    }
}
