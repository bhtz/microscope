using GraphQL.Types;
using IronHasura.Data;

namespace IronHasura.GraphQL.Types
{
    public class RemoteConfigType : ObjectGraphType<RemoteConfig>
    {
        public RemoteConfigType()
        {
            Field(x => x.Id, type:typeof(StringGraphType));
            Field(x => x.Key);
            Field(x => x.Value);
        }
    }
}