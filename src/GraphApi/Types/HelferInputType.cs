using GraphQL.Types;
using Hilfswerk.Models;

namespace Hilfswerk.GraphApi
{
    public class HelferInputType : InputObjectGraphType<HelferCreateModel>
    {
        public HelferInputType()
        {
            Name = "HelferInput";
            Field(p => p.Anmerkung);
            Field<KontaktInputType>("kontakt", resolve: context => context.Source.Kontakt);
            Field<ListGraphType<TaetigkeitEnumType>>("taetigkeiten", resolve: context => context.Source.Taetigkeiten);
            Field(p => p.hatAuto);
            Field(p => p.istRisikogruppe);
        }
    }
}
