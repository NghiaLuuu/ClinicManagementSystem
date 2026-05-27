namespace AuthService.Infrastructure.Persistence.Configurations;

using AuthService.Domain.Entities;
using AuthService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);

        builder.Ignore(x => x.DomainEvents);

        var usernameConverter = new ValueConverter<Username, string>(
            v => v.Value,
            v => Username.Create(v));

        var emailConverter = new ValueConverter<Email, string>(
            v => v.Value,
            v => Email.Create(v));

        var passwordHashConverter = new ValueConverter<PasswordHash, string>(
            v => v.Value,
            v => PasswordHash.Create(v));

        builder.Property(x => x.Username)
            .HasConversion(usernameConverter)
            .HasColumnName("username")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion(emailConverter)
            .HasColumnName("email")
            .IsRequired();

        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.PasswordHash)
            .HasConversion(passwordHashConverter)
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Property(x => x.Role)
            .HasConversion<int>()
            .HasColumnName("role")
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(x => x.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        builder.Property(x => x.UpdatedBy).HasColumnName("updated_by");
    }
}