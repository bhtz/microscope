using GraphQL.Types;

public class RootQuery : ObjectGraphType<object>
{
    public RootQuery()
    {
        Name = "Query";
        
        Field<StringGraphType>("hello", resolve: (context) => "hello world !");
    }
}