using deneme_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace deneme_2.Configurations
{
    public class CatagoryConfiguration : IEntityTypeConfiguration<Catagory>
    {
        public void Configure(EntityTypeBuilder<Catagory> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(500);
            builder.HasMany(c => c.Books)
                   .WithOne(b => b.Catagory)
                   .HasForeignKey(b => b.CatagoryId);
        }
    }
    
    
}
