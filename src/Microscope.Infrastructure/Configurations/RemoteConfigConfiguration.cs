using Microscope.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microscope.Infrastructure.Configurations
{
    public class RemoteConfigConfiguration : IEntityTypeConfiguration<RemoteConfig>
    {
        public void Configure(EntityTypeBuilder<RemoteConfig> builder)
        {         
            builder.Ignore(b => b.DomainEvents);

            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Dimension)
                .IsRequired()
                .HasColumnType<string>("jsonb");
        }
    }
}