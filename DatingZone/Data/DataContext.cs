using DatingZone.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingZone.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<AppUser> tblUsers { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
