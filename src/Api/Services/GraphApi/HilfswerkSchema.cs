using GraphQL;
using GraphQL.Types;
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
