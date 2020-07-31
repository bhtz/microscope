using System.Collections.Generic;
using GraphQL.Types;
using Microscope.GraphQL;

public class RootMutation : ObjectGraphType<object>
{
    public RootMutation(IEnumerable<IGraphMutationMarker> markers)
    {
        foreach(var marker in markers)
        {
            var q = marker as ObjectGraphType<object>;
            foreach(var f in q.Fields)
            {
                AddField(f);
            }
        }
    }
}