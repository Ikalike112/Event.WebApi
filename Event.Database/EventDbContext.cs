using Event.Application.Interfaces;
using Event.Database.EntityTypeConfigurations;
using Event.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Database
{
    public class EventDbContext : IdentityDbContext<Speaker>, IEventDbContext
    {
        public DbSet<Domain.Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public EventDbContext(DbContextOptions<EventDbContext> options)
            : base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EventConfiguration());
            base.OnModelCreating(builder);
            builder.Entity<Speaker>(entity => entity.ToTable(name: "Users"));
            builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
            builder.Entity<IdentityUserRole<string>>(entity =>
                entity.ToTable(name: "UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(entity =>
                entity.ToTable(name: "UserClaim"));
            builder.Entity<IdentityUserLogin<string>>(entity =>
                entity.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<string>>(entity =>
                entity.ToTable("UserTokens"));
            builder.Entity<IdentityRoleClaim<string>>(entity =>
                entity.ToTable("RoleClaims"));
            builder.ApplyConfiguration(new SpeakerConfiguration());
        }
    }
}
