using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltaMedia.Model
{
    public class AltaMediaDbContext : DbContext
    {
        public AltaMediaDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<BookTicket> BookTickets { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
