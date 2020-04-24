using GraphQL.Types;
using IronHasura.Data;
using IronHasura.GraphQL.Types;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class RootMutation : ObjectGraphType<object>
{
    public RootMutation()
    {
        Field<IdentityMutation>("identity", resolve: context => new {});
    }
}