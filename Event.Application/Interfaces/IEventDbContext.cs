using Event.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Event.Application.Interfaces
{
    public interface IEventDbContext
    {
        public DbSet<Domain.Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken token);
    }
}
