using API_JWT.Model;
using Microsoft.EntityFrameworkCore;

namespace API_JWT.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
