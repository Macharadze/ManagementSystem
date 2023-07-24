using CRUDAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Dal
{
    public class MyDB : DbContext
    {
        public MyDB(DbContextOptions options):base(options) {
        }
        public DbSet<User> users { get; set; }
        public DbSet<UserProfile> userProfiles { get; set; }
    }
}
