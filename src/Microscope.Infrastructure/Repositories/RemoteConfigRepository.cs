using Microscope.Domain.Aggregates.RemoteConfigAggregate;
using Microscope.Domain.Entities;

namespace Microscope.Infrastructure.Repositories
{
    public class RemoteConfigRepository : EFRepositoryBase<RemoteConfig>, IRemoteConfigRepository
    {
        public RemoteConfigRepository(MicroscopeDbContext context) : base(context)
        {
            
        }
    }
}
