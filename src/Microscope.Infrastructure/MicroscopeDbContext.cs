using System.Reflection;
using MediatR;
using Microscope.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Microscope.Infrastructure
{
    public class MicroscopeDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MicroscopeDbContext> _logger;

        #region DbSets

        public virtual DbSet<Analytic> Analytics { get; set; }
        public virtual DbSet<RemoteConfig> RemoteConfigs { get; set; }
        
        #endregion
        
        public MicroscopeDbContext(DbContextOptions<MicroscopeDbContext> options, IMediator mediator, ILogger<MicroscopeDbContext> logger) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
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


    }
}
