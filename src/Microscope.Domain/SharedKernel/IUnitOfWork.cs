using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microscope.Domain.SharedKernel
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveChangesAndDispatchEventsAsync(CancellationToken cancellationToken = default(CancellationToken));   
    }
}
