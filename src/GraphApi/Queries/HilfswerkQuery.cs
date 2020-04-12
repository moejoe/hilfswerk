using GraphQL;
using GraphQL.Authorization;
using GraphQL.Types;
using Hilfswerk.Core.Stores;
using Microsoft.Extensions.Options;
using System;

namespace Hilfswerk.GraphApi.Queries
{
    public class GraphQLAuthorizationOptions 
    {
        public string AuthorizationPolicy { get; set; }
    }
    public class HilfswerkQuery : ObjectGraphType
    {
        public HilfswerkQuery(IHelferStore store, IOptionsSnapshot<GraphQLAuthorizationOptions> options)
        {
            if(!string.IsNullOrWhiteSpace(options.Value.AuthorizationPolicy)) {
                this.AuthorizeWith(options.Value.AuthorizationPolicy);
            }
            
            Name = "Query";

            FieldAsync<ListGraphType<HelferType>>(
                name: "helferByName",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "nameSearchTerms" }),
                resolve: async context =>
                {
                    return await store.FindByName(context.GetArgument<string>("nameSearchTerms"));
                }
            );

            FieldAsync<HelferType>(
                name: "helferById",
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "helferId" }),
                resolve: async context =>
                {
                    return await store.GetHelfer(context.GetArgument<string>("helferId"));
                }
            );

            FieldAsync<ListGraphType<HelferType>>(
                name: "helfer",
                arguments: new QueryArguments(new QueryArgument<ListGraphType<IntGraphType>> { Name = "inPlz" },
                    new QueryArgument<ListGraphType<TaetigkeitEnumType>> { Name = "taetigkeitIn" },
                    new QueryArgument<BooleanGraphType> { Name = "istRisikoGruppe" },
                    new QueryArgument<BooleanGraphType> { Name = "hatAuto" },
                    new QueryArgument<BooleanGraphType> { Name = "istZivildiener" },
                    new QueryArgument<BooleanGraphType> { Name = "istFreiwilliger" },
                    new QueryArgument<BooleanGraphType> { Name = "istAusgelastet" },
                    new QueryArgument<BooleanGraphType> { Name = "istDSGVOKonform" }
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
                        IstAusgelastetFilter = context.GetArgument<bool?>("istAusgelastet"),
                        IstDSGVOKonform = context.GetArgument<bool?>("istDSGVOKonform")
                    };
                    return await store.FindHelfer(filter);
                }
            );
        }
    }
}
