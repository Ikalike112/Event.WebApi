using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Event.Database.EntityTypeConfigurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Domain.Event>
    {
        public void Configure(EntityTypeBuilder<Domain.Event> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Description).HasMaxLength(1024);
        }
    }
}
