using GraphQL.Types;
using Hilfswerk.Models;

namespace Hilfswerk.GraphApi
{
    public class CreateHelferInputType : InputObjectGraphType<HelferCreateModel>
    {
        public CreateHelferInputType()
        {
            Name = "CreateHelferInput";
            Field(p => p.Anmerkung);
            Field<KontaktInputType>("kontakt", resolve: context => context.Source.Kontakt);
            Field<ListGraphType<TaetigkeitEnumType>>("taetigkeiten", resolve: context => context.Source.Taetigkeiten);
            Field(p => p.hatAuto);
            Field(p => p.istRisikogruppe);
            Field(p => p.istZivildiener);
            Field(p => p.istFreiwilliger);
            Field(p => p.istAusgelastet);
        }
    }
}
