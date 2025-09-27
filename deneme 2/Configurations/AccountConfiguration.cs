using deneme_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace deneme_2.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(u => u.FullName)
       .IsRequired()
       .HasMaxLength(200);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(256);
            builder.HasIndex(u => u.Email).IsUnique();

            //builder.Property(u => u.UserName)
            //       .IsRequired()
            //       .HasMaxLength(256);
            //builder.HasIndex(u => u.UserName).IsUnique();

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(15);
            // builder.HasIndex(u => u.PhoneNumber).IsUnique(); // lazım olsa saxlayın

            builder.Property(u => u.PasswordHash)
                   .IsRequired(); // uzunluq limitini ya çıxarın, ya da >256 seçin

        }
    }
}
