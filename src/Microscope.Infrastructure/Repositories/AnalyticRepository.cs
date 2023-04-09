using Microscope.Domain.Aggregates.Analytic;
using Microscope.Domain.Aggregates.AnalyticAggregate;
using Microscope.Infrastructure.Repositories.Base;

namespace Microscope.Infrastructure.Repositories
{
    public class AnalyticRepository : EFRepositoryBase<Analytic>, IAnalyticRepository
    {
        public AnalyticRepository(MicroscopeDbContext context) : base(context)
        {
            
        }  
    }
}
