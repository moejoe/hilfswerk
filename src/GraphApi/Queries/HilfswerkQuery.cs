using GraphQL;
using GraphQL.Types;
using Hilfswerk.Core.Stores;
using System;
using System.Linq;

namespace Hilfswerk.GraphApi.Queries
{
    public class HilfswerkQuery : ObjectGraphType
    {
        public HilfswerkQuery(IHelferStore store)
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

                    var filter = new HelferFilter
                    {
                        PlzFilter = context.GetArgument<int[]>("inPlz") ?? Array.Empty<int>(),
                        TaetigkeitFilter = context.GetArgument<Models.Taetigkeit[]>("taetigkeitIn") ?? Array.Empty<Models.Taetigkeit>(),
                        HatAutoFilter = context.GetArgument<bool?>("hatAuto"),
                        IstRisikoGruppeFilter = context.GetArgument<bool?>("istRisikoGruppe")
                    };
                    return await store.FindHelfer(filter);
                }
            );
        }
    }
}
