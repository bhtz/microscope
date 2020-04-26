using System.Collections.Generic;
using GraphQL.Types;
using IronHasura.GraphQL;

public class RootQuery : ObjectGraphType<object>
{
    public RootQuery(IEnumerable<IGraphQueryMarker> markers)
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