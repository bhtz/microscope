using GraphQL.Authorization;
using System.Security.Claims;

namespace Microscope.GraphQL
{
    public class GraphQLUserContext : IProvideClaimsPrincipal
    {
        public ClaimsPrincipal User { get; set; }
    }
}
