using deneme_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace deneme_2.Coonfigurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Description).HasMaxLength(1000);
            builder.Property(b => b.ReleaseDate).IsRequired();
            builder.Property(b => b.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasOne(b => b.Author)
                   .WithMany(a => a.Books)
                   .HasForeignKey(b => b.AuthorId);
                   
            builder.HasOne(b => b.Catagory)
                   .WithMany(c => c.Books)
                   .HasForeignKey(b => b.CatagoryId);
                   

        }
    }
}
