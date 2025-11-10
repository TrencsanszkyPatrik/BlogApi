using Microsoft.EntityFrameworkCore;

namespace BlogApi.Models
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext()
        {

        }

        public BlogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Blogger> bloggers { get; set; }
    }
}
