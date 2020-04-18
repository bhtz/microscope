using GraphQL;
using GraphQL.Types;

namespace IronHasura.GraphQL
{
    public class IronHasuraSchema : Schema
    {
        public IronHasuraSchema(IDependencyResolver resolver) : base(resolver)
        {
            //Query = resolver.Resolve<MyHotelQuery>();
        }
    }
}