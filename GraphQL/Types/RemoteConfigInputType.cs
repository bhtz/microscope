using GraphQL.Types;
using Microscope.Data;


public class RemoteConfigInputType : InputObjectGraphType<RemoteConfig>
{

    public RemoteConfigInputType()
    {
        Field(x => x.Id);
        Field(x => x.Key);
        Field(x => x.Value);
    }
}
