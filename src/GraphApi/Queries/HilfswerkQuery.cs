using GraphQL;
using GraphQL.Authorization;
using GraphQL.Types;
using Hilfswerk.Core.Stores;
using System;

namespace Hilfswerk.GraphApi.Queries
{
    public class HilfswerkQuery : ObjectGraphType
    {
        public HilfswerkQuery(IHelferStore store)
        {
            this.AuthorizeWith("DefaultPolicy");
            Name = "Query";

            FieldAsync<ListGraphType<HelferType>>(
                name: "helfer",
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IntGraphType>> { Name = "inPlz" },
                    new QueryArgument<ListGraphType<TaetigkeitEnumType>> { Name = "taetigkeitIn" },
                    new QueryArgument<BooleanGraphType> { Name = "istRisikoGruppe" },
                    new QueryArgument<BooleanGraphType> { Name = "hatAuto" },
                    new QueryArgument<BooleanGraphType> { Name = "istZivildiener" },
                    new QueryArgument<BooleanGraphType> { Name = "istFreiwilliger" },
                    new QueryArgument<BooleanGraphType> { Name = "istAusgelastet" }
                    ),
                resolve: async context =>
                {

                    var filter = new HelferFilter
                    {
                        PlzFilter = context.GetArgument<int[]>("inPlz") ?? Array.Empty<int>(),
                        TaetigkeitFilter = context.GetArgument<Models.Taetigkeit[]>("taetigkeitIn") ?? Array.Empty<Models.Taetigkeit>(),
                        HatAutoFilter = context.GetArgument<bool?>("hatAuto"),
                        IstRisikoGruppeFilter = context.GetArgument<bool?>("istRisikoGruppe"),
                        IstZivildienerFilter = context.GetArgument<bool?>("istZivildiener"),
                        IstFreiwilligerFilter = context.GetArgument<bool?>("istFreiwilliger"),
                        IstAusgelastetFilter = context.GetArgument<bool?>("istAusgelastet")
                    };
                    return await store.FindHelfer(filter);
                }
            );
        }
    }
}
