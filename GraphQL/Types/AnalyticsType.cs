using GraphQL.Types;
using IronHasura.Data;

namespace IronHasura.GraphQL.Types
{
    public class AnalyticType : ObjectGraphType<Analytic>
    {
        public AnalyticType()
        {
            Field(x => x.Id, type:typeof(StringGraphType));
            Field(x => x.Key);
            Field(x => x.Dimension);
        }
    }
}