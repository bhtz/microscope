using GraphQL.Types;
using Microsoft.AspNetCore.Identity;

namespace IronHasura.GraphQL.Types 
{
    public class UserType : ObjectGraphType<IdentityUser>
    {
        public UserType(UserManager<IdentityUser> userManager)
        {
            Field(x => x.Id);
            Field(x => x.UserName);
            Field(x => x.Email);
        }
    }
}