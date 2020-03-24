using GraphQL;
using GraphQL.Types;
using Hilfswerk.Core.Models;
using Hilfswerk.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hilfswerk.GraphApi
{
    public class HilfswerkMutation : ObjectGraphType
    {
        public HilfswerkMutation(HilfswerkDbContext db)
        {
            Name = "Mutation";

            FieldAsync<HelferType>("createHelfer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<HelferInputType>> { Name = "helfer" }),
                resolve: async context =>
                {
                    var helfer = context.GetArgument<HelferCreateModel>("helfer").ToEntity(Guid.NewGuid().ToString());
                    db.Add(helfer);
                    await db.SaveChangesAsync();
                    return helfer;
                });
            FieldAsync<EinsatzType>("createEinsatz",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "helferId" },
                    new QueryArgument<NonNullGraphType<EinsatzInputType>> { Name = "einsatz" }
                ),
                resolve: async context =>
                {
                    var helferId = context.GetArgument<string>("helferId");
                    var helfer = await db.Helfer.SingleOrDefaultAsync(p => p.Id == helferId) ?? throw new InvalidOperationException($"Helfer {helferId} not found");
                    var einsatz = new EntityFramework.Entities.Einsatz
                    {
                        Helfer = helfer,
                        Id = Guid.NewGuid().ToString(),
                        VermitteltAm = DateTime.Now,
                        VermitteltDurch = "Ein Benutzer"
                    };
                    context.GetArgument<EinsatzCreateModel>("einsatz").ApplyTo(einsatz);
                    db.Add(einsatz);
                    await db.SaveChangesAsync();
                    return einsatz;
                });
        }
    }

    public class EinsatzInputType : InputObjectGraphType<EinsatzCreateModel>
    {
        public EinsatzInputType()
        {
            Name = "EinsatzInput";
            Field(p => p.Hilfesuchender);
            Field<TaetigkeitEnumType>("taetigkeit", resolve: p => p.Source.Taetigkeit);
            Field(p => p.VermitteltDurch);
            Field(p => p.Anmerkungen);
        }
    }

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
