using GraphQL.Authorization;
using GraphQL.Types;
using Hilfswerk.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hilfswerk.GraphApi
{
    public class HilfswerkQuery : ObjectGraphType<object>
    {
        private readonly List<Helfer> _helfer;
        public HilfswerkQuery(Helfer[] helfer)
        {
            _helfer = helfer.ToList();

            Name = "Query";

            Field<ListGraphType<HelferType>>(
                name: "helfer",
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IntGraphType>> { Name = "inPlz" },
                    new QueryArgument<ListGraphType<TaetigkeitEnumType>> { Name = "taetigkeitIn" },
                    new QueryArgument<BooleanGraphType> { Name = "istRisikoGruppe" },
                    new QueryArgument<BooleanGraphType> { Name = "hatAuto" }
                    ),
                resolve: context =>
                {
                    var plzList = context.GetArgument<int[]>("inPlz") ?? Array.Empty<int>();
                    var helferQuery = _helfer.AsEnumerable();
                    if (plzList.Any())
                    {
                        helferQuery = helferQuery.Where(p => plzList.Contains(p.Kontakt.Plz));
                    }
                    var taetigkeiten = context.GetArgument<Taetigkeit[]>("taetigkeitIn") ?? Array.Empty<Taetigkeit>();
                    if (taetigkeiten.Any())
                    {
                        helferQuery = helferQuery.Where(p => taetigkeiten.All(q => p.Taetigkeiten.Contains(q)));
                    }
                    var istRisikoGruppeFilter = context.GetArgument<bool?>("istRisikoGruppe");
                    if (istRisikoGruppeFilter.HasValue)
                    {

                    }
                    var hatAutoFilter = context.GetArgument<bool?>("hatAuto");
                    if (hatAutoFilter.HasValue)
                    {
                        helferQuery = helferQuery.Where(p => p.hatAuto == hatAutoFilter.Value);
                    }
                    return helferQuery.ToArray();
                }
            ).AuthorizeWith("Authenticated");

        }
    }
}
