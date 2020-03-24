using GraphQL;
using GraphQL.Types;
using Hilfswerk.Core.Stores;
using Hilfswerk.Models;

namespace Hilfswerk.GraphApi.Mutations
{
    public class HilfswerkMutation : ObjectGraphType
    {
        public HilfswerkMutation(IHelferStore store)
        {
            Name = "Mutation";

            FieldAsync<HelferType>("createHelfer",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<HelferInputType>> { Name = "helfer" }),
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
        }
    }
}
