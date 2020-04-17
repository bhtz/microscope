using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IronHasura.Data;
using Microsoft.AspNetCore.Identity;

namespace IronHasura.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<IdentityUser> _userManager;
        
        public ProfileService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}