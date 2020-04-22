using GraphQL.Types;
using Microsoft.AspNetCore.Identity;

namespace IronHasura.GraphQL.Types 
{
    public class RoleType : ObjectGraphType<IdentityRole>
    {
        public RoleType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
        }
    }
}