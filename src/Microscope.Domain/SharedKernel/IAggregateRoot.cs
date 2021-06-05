using System;

namespace Microscope.Domain.SharedKernel
{
    public interface IAggregateRoot : IAggregateRoot<Guid>
    {
        
    }

    public interface IAggregateRoot<TId>
    {
        
    }
}
