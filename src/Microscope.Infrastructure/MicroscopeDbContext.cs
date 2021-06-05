using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microscope.Domain.Entities;
using Microscope.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Microscope.Infrastructure
{
    public class MicroscopeDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        #region DbSets

        public virtual DbSet<Analytic> Analytics { get; set; }
        public virtual DbSet<RemoteConfig> RemoteConfigs { get; set; }
        
        #endregion
        
        public MicroscopeDbContext(DbContextOptions<MicroscopeDbContext> options, IMediator mediator) : base(options)
        {
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public void Migrate()
        {
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("mcsp_common");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public async Task<bool> SaveChangesAndDispatchEventsAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = this.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await this._mediator.Publish(domainEvent);

            var result = await base.SaveChangesAsync(cancellationToken);
            
            return true;
        }
    }
}
