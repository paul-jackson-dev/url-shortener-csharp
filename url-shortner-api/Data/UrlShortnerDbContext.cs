using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using url_shortner_api.Models;

namespace url_shortner_api.Data
{
    public class UrlShortnerDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {

        public DbSet<UrlInfo> UrlInfo { get; set; }

        public UrlShortnerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Event>()
            //    .HasOne(p => p.Category)
            //    .WithMany(b => b.events);

            //modelBuilder.Entity<Event>()
            //    .HasMany(p => p.Tags)
            //    .WithMany(p => p.Events)
            //    .UsingEntity(j => j.ToTable("EventTags"));

            base.OnModelCreating(modelBuilder); // pass modelBuilder to base class
        }

    }
}
