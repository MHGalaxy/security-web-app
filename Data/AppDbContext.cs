using Cyber_Security_App.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cyber_Security_App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null!;
    }
}
