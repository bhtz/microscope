using GraphQL.Authorization;
using System.Threading.Tasks;

namespace Microscope.Authorization.Graphql
{
    public class AuthenticatedAuthorizationRequirement : IAuthorizationRequirement
    {
        public Task Authorize(AuthorizationContext context)
        {
          
            if (!context.User.Identity.IsAuthenticated)
            {
                context.ReportError("Not authenticated");
            }

            return Task.CompletedTask;
        }
    }
}
