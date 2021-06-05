using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microscope.Domain.SharedKernel
{

    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }

    public interface IRepository<T> : IRepository<T, Guid> where T : class, IAggregateRoot<Guid>
    {

    }

    public interface IRepository<T, TId> : IRepository where T : class, IAggregateRoot<TId>
    {

    }

    public interface IBaseRepository<T, TId> : IRepository where T : class, IAggregateRoot<TId>
    {
        Task<T> GetByIdAsync(TId id);

        Task<List<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }

    public interface IBaseRepository<T> : IBaseRepository<T, Guid> where T : class, IAggregateRoot<Guid>
    {
        
    }
}
