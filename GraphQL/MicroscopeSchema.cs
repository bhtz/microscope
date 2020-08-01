using GraphQL;
using GraphQL.Types;

namespace Microscope.GraphQL
{
    public class MicroscopeSchema : Schema
    {
        public MicroscopeSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RootQuery>();
            Mutation = resolver.Resolve<RootMutation>();
        }
    }
}