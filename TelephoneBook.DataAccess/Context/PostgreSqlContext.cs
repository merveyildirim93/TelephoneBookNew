using Microsoft.EntityFrameworkCore;
using TelephoneBook.DataAccess.Entity;

namespace TelephoneBook.DataAccess.Context
{
    public class PostreSqlContext : DbContext
    {
        public PostreSqlContext(DbContextOptions<PostreSqlContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostreSqlContext).Assembly);
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Report> Report { get; set; }
    }
}
