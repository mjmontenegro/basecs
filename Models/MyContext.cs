using Microsoft.EntityFrameworkCore;
 
namespace crudi.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        // users table represented below
        public DbSet<User> Users {get;set;}
    }
}