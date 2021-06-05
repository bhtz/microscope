using Microscope.Domain.Aggregates.AnalyticAggregate;
using Microscope.Domain.Entities;

namespace Microscope.Infrastructure.Repositories
{
    public class AnalyticRepository : EFRepositoryBase<Analytic>, IAnalyticRepository
    {
        public AnalyticRepository(MicroscopeDbContext context) : base(context)
        {
            
        }  
    }
}
