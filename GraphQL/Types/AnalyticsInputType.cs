using GraphQL.Types;
using Microscope.Data;



public class AnalyticsInputType : InputObjectGraphType<Analytic>
{

    public AnalyticsInputType()
    {
        Field(x => x.Id);
        Field(x => x.Key);
        Field(x => x.Dimension);
    }
}
