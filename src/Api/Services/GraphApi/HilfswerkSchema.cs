using GraphQL;
using GraphQL.Types;

namespace Hilfswerk.GraphApi
{
    public class HilfswerkSchema : Schema
    {
        public HilfswerkSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<HilfswerkQuery>();
            Mutation = resolver.Resolve<HilfswerkMutation>();
        }
    }
}
