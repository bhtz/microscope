using GraphQL.Authorization;
using System.Security.Claims;
using System.Collections.Generic;


namespace Microscope.GraphQL
{
    public class GraphQLUserContext : Dictionary<string, object>, IProvideClaimsPrincipal
    {   
        public ClaimsPrincipal User { get; set; }
    }
}
