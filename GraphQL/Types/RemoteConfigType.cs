using GraphQL.Types;
using Microscope.Data;

namespace Microscope.GraphQL.Types
{
    public class RemoteConfigType : ObjectGraphType<RemoteConfig>
    {
        public RemoteConfigType()
        {
            Field(x => x.Id, type:typeof(StringGraphType)).Name("Id");;
            Field(x => x.Key).Name("Key");
            Field(x => x.Value).Name("Value");
        }
    }
}