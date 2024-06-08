using Lab10.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
    }
}
