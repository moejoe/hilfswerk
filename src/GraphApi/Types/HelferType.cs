using GraphQL.Types;
using Hilfswerk.Models;
using Hilfswerk.Core.Stores;
using System.Linq;

namespace Hilfswerk.GraphApi
{
    public class HelferType : ObjectGraphType<Helfer>
    {
        public HelferType(IHelferStore store)
        {
            Name = "Helfer";
            Field(x => x.Id)
                .Description("Id");
            Field<KontaktType>("kontakt", resolve: context => context.Source.Kontakt);
            Field(x => x.Anmerkung);
            FieldAsync<ListGraphType<TaetigkeitEnumType>>("taetigkeiten",
                resolve: async context =>
                {
                    return await store.GetTaetigkeiten(context.Source.Id);
                });
            Field<ListGraphType<EinsatzType>>("einsaetze",
                resolve: context =>
                {
                    return context.Source.Einsaetze;
                }
            );
            Field<IntGraphType>("totalEinsaetze",
                resolve: context =>
                {
                    return context.Source.Einsaetze.Count();
                }
            );
            Field(x => x.hatAuto);
            Field(x => x.istRisikogruppe);
            Field(x => x.istFreiwilliger);
            Field(x => x.istZivildiener);
            Field(x => x.istAusgelastet);
            Field(x => x.istDSGVOKonform);
        }
    }
}
