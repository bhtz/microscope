using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microscope.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Microscope.Infrastructure.Repositories
{
    public class EFRepositoryBase<TEntity> : EFRepositoryBase<TEntity, Guid>, IBaseRepository<TEntity> where TEntity : class, IAggregateRoot<Guid>
    {
        public EFRepositoryBase(MicroscopeDbContext context) : base(context)
        {
        }
    }

    public class EFRepositoryBase<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : class, IAggregateRoot<TPrimaryKey>
    {

        protected readonly MicroscopeDbContext _context;


        public EFRepositoryBase(MicroscopeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        private DbSet<TEntity> DbSet => _context.Set<TEntity>();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var added = await DbSet.AddAsync(entity);
            return added.Entity;
        }

        public Task DeleteAsync(TEntity entity)
        {
            AttachIfNot(entity);
            DbSet.Remove(entity);

            return Task.FromResult(true);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.AsQueryable().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return await DbSet.FindAsync(id);
        }

        public Task UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        private void AttachIfNot(TEntity entity)
        {
            var entry = _context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            DbSet.Attach(entity);
        }
    }
}
