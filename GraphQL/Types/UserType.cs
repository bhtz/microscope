using GraphQL.Types;
using Microsoft.AspNetCore.Identity;

namespace Microscope.GraphQL.Types 
{
    public class UserType : ObjectGraphType<IdentityUser>
    {
        public UserType()
        {
            Field(x => x.Id);
            Field(x => x.UserName);
            Field(x => x.Email);
        }
    }
}