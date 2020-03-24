using GraphQL.Types;
using Hilfswerk.GraphApi.Mutations;
using Hilfswerk.GraphApi.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hilfswerk.GraphApi
{
    public class HilfswerkSchema : Schema
    {
        public HilfswerkSchema(IServiceProvider services) : base(services)
        {
            Query = services.GetRequiredService<HilfswerkQuery>();
            Mutation = services.GetRequiredService<HilfswerkMutation>();
        }
    }
}
