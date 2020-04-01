using GraphQL.Types;
using Hilfswerk.Models;
using Hilfswerk.Core.Stores;

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
            FieldAsync<ListGraphType<EinsatzType>>("einsaetze", 
                resolve: async context =>
                {
                    return await store.GetEinsaetze(context.Source.Id);
                }
            );
            FieldAsync<IntGraphType>("totalEinsaetze", 
                resolve: async context =>
                {
                    return await store.CountEinsaetze(context.Source.Id);
                }
            );
            Field(x => x.hatAuto);
            Field(x => x.istRisikogruppe);
            Field(x => x.istFreiwilliger);
            Field(x => x.istZivildiener);
            Field(x => x.istAusgelastet);
        }
    }
}
