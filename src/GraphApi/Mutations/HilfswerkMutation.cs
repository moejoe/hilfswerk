using GraphQL;
using GraphQL.Authorization;
using GraphQL.Types;
using Hilfswerk.Core.Stores;
using Hilfswerk.Models;
using System;

namespace Hilfswerk.GraphApi.Mutations
{
    public class HilfswerkMutation : ObjectGraphType
    {
        public HilfswerkMutation(IHelferStore store)
        {
            Name = "Mutation";
            this.AuthorizeWith("DefaultPolicy");
            FieldAsync<HelferType>("createHelfer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CreateHelferInputType>> { Name = "helfer" }),
                resolve: async context =>
                {
                    var helfer = context.GetArgument<HelferCreateModel>("helfer");
                    return await store.AddHelfer(helfer);
                });
            FieldAsync<EinsatzType>("createEinsatz",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "helferId" },
                    new QueryArgument<NonNullGraphType<EinsatzInputType>> { Name = "einsatz" }
                ),
                resolve: async context =>
                {
                    var helferId = context.GetArgument<string>("helferId");
                    var einsatz = context.GetArgument<EinsatzCreateModel>("einsatz");
                    return await store.AddEinsatz(helferId, einsatz);
                });
            FieldAsync<EinsatzType>("editEinsatz",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "helferId" },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "einsatzId" },
                    new QueryArgument<NonNullGraphType<EinsatzEditInputType>> { Name = "einsatz" }),
                resolve: async context =>
                {
                    var helferId = context.GetArgument<string>("helferId");
                    var einsatzId = context.GetArgument<string>("einsatzId");
                    var einsatz = context.GetArgument<EinsatzEditModel>("einsatz");
                    return await store.EditEinsatz(helferId, einsatzId, einsatz);
                });
            FieldAsync<BooleanGraphType>("editHelfer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<EditHelferInputType>> { Name = "helfer" },
                    new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: async context =>
                {
                    var helfer = context.GetArgument<HelferEditModel>("helfer");
                    var id = context.GetArgument<string>("id");
                    try
                    {
                        await store.EditHelfer(id, helfer);
                        return true;
                    }
                    catch (InvalidOperationException ex)
                    {
                        return false;
                    }
                });
            FieldAsync<BooleanGraphType>("setAusgelastet",
                arguments: new QueryArguments(
                    new QueryArgument<BooleanGraphType> { Name = "istAusgelastet" },
                    new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: async context =>
                {
                    var istAusgelastet = context.GetArgument<bool>("istAusgelastet");
                    var id = context.GetArgument<string>("id");
                    try
                    {
                        await store.SetAusgelastet(id, istAusgelastet);
                        return true;
                    }
                    catch (InvalidOperationException)
                    {
                        return false;
                    }
                });
        }
    }
}
