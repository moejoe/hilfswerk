using GraphQL;
using GraphQL.Authorization;
using GraphQL.Types;
using Hilfswerk.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Hilfswerk.GraphApi
{
    public class HilfswerkQuery : ObjectGraphType<object>
    {
        public HilfswerkQuery(HilfswerkDbContext db)
        {
            Name = "Query";

            FieldAsync<ListGraphType<HelferType>>(
                name: "helfer",
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IntGraphType>> { Name = "inPlz" },
                    new QueryArgument<ListGraphType<TaetigkeitEnumType>> { Name = "taetigkeitIn" },
                    new QueryArgument<BooleanGraphType> { Name = "istRisikoGruppe" },
                    new QueryArgument<BooleanGraphType> { Name = "hatAuto" }
                    ),
                resolve: async context =>
                {
                    var plzList = context.GetArgument<int[]>("inPlz") ?? Array.Empty<int>();
                    var helferQuery = from helfer in db.Helfer
                                        .Include(p => p.HelferTaetigkeiten)
                                      select helfer;
                    if (plzList.Any())
                    {
                        helferQuery = helferQuery.Where(p => plzList.Contains(p.Kontakt.Plz));
                    }
                    var taetigkeiten = (context.GetArgument<Core.Models.Taetigkeit[]>("taetigkeitIn") ?? Array.Empty<Core.Models.Taetigkeit>()).Select(p => (int)p).ToList();
                    if (taetigkeiten.Any())
                    {
                        helferQuery = helferQuery.Where(p => p.HelferTaetigkeiten.Any(q => taetigkeiten.Contains(q.TaetigkeitId)));
                    }
                    var istRisikoGruppeFilter = context.GetArgument<bool?>("istRisikoGruppe");
                    if (istRisikoGruppeFilter.HasValue)
                    {
                        helferQuery = helferQuery.Where(p => p.istRisikogrupepe == istRisikoGruppeFilter.Value);
                    }
                    var hatAutoFilter = context.GetArgument<bool?>("hatAuto");
                    if (hatAutoFilter.HasValue)
                    {
                        helferQuery = helferQuery.Where(p => p.hatAuto == hatAutoFilter.Value);
                    }
                    return await helferQuery.ToArrayAsync();

                }
            ).AuthorizeWith("Authenticated");

        }
    }
}
