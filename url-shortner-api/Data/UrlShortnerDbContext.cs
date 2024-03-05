using Microsoft.EntityFrameworkCore;
using url_shortner_api.Models;

namespace url_shortner_api.Data
{
    public class UrlShortnerDbContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }

        public UrlShortnerDbContext(DbContextOptions options) : base(options) { }

    }
}
