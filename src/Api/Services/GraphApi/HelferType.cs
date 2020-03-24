using GraphQL.Types;
using Hilfswerk.EntityFramework;
using Hilfswerk.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Hilfswerk.GraphApi
{
    public class HelferType : ObjectGraphType<Helfer>
    {
        public HelferType(HilfswerkDbContext db)
        {
            Name = "Helfer";
            Field(x => x.Id)
                .Description("Id");
            Field<KontaktType>("kontakt", resolve: context => context.Source.Kontakt);
            Field(x => x.Anmerkung);
            Field<ListGraphType<TaetigkeitEnumType>>("taetigkeiten", resolve: context => context.Source.HelferTaetigkeiten.Select(p => (Core.Models.Taetigkeit)p.TaetigkeitId));

            FieldAsync<ListGraphType<EinsatzType>>("einsaetze", resolve:
                async context =>
                {
                    return await db.Einsaetze.Where(p => p.Helfer.Id == context.Source.Id).ToArrayAsync();
                }
            );
            FieldAsync<IntGraphType>("totalEinsaetze", resolve:
                async context =>
                {
                    return await db.Einsaetze.Where(p => p.Helfer.Id == context.Source.Id).CountAsync();
                }
            );
            Field(x => x.hatAuto);
            Field(x => x.istRisikogrupepe);
        }
    }
}
