using Event.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Database
{
    public class DbInitializer
    {
        public static void Initialize (IdentityDbContext<Speaker> db)
        {
            db.Database.EnsureCreated();
        }
    }
}
