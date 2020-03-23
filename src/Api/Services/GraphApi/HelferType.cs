using GraphQL.Types;
using Hilfswerk.Core.Models;
using System.Linq;

namespace Hilfswerk.GraphApi
{
    public class HelferType : ObjectGraphType<Helfer>
    {
        public HelferType()
        {
            Name = "Helfer";
            Field(x => x.Id)
                .Description("Id");
            Field<KontaktType>("kontakt", resolve: context => context.Source.Kontakt);
            Field(x => x.Anmerkung);
            Field<ListGraphType<TaetigkeitEnumType>>("taetigkeiten", resolve: context => context.Source.Taetigkeiten);
            Field<ListGraphType<EinsatzType>>("einsaetze", resolve: context => context.Source?.Einsaetze);
            Field<IntGraphType>("totalEinsaetze", resolve: context => context.Source?.Einsaetze?.Count() ?? 0);
            Field(x => x.hatAuto);
        }
    }

    public class TaetigkeitEnumType : EnumerationGraphType<Taetigkeit>
    {
    }
}
