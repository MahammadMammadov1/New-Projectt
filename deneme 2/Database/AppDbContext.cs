using deneme_2.Coonfigurations;
using deneme_2.Validations.BookValidators;
using Microsoft.EntityFrameworkCore;

namespace deneme_2.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Models.Author> Authors { get; set; }
        public DbSet<Models.Book> Books { get; set; }
        public DbSet<Models.Catagory> Catagories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);


        }
    }
}
