using Microsoft.EntityFrameworkCore;

namespace deneme_2.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Models.Author>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Models.Author> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.BirthDate).HasColumnType("Date");
            builder.HasMany(a => a.Books)
                   .WithOne(b => b.Author)
                   .HasForeignKey(b => b.AuthorId);
        }
    }
    
}
