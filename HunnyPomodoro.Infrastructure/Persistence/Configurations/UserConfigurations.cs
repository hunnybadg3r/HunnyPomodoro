using HunnyPomodoro.Domain.User;
using HunnyPomodoro.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberDinner.Infrastructure.Persistence.Configurations;

public sealed class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
        ConfigureUsersResultsTable(builder);
    }

    private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users");

        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder
            .Property(u => u.Name)
            .HasMaxLength(50);

        builder
            .Property(u => u.Email)
            .HasMaxLength(150);

        builder
            .Property(u => u.Password);
    }

    private static void ConfigureUsersResultsTable(EntityTypeBuilder<User> builder)
    {
        builder.OwnsMany(h => h.Sessions, sib =>
        {
            sib.WithOwner().HasForeignKey("UserId");

            sib.ToTable("UserSessionIds");

            sib.HasKey("Id");

            sib.Property(si => si.Value)
                .ValueGeneratedNever()
                .HasColumnName("UserSessionIds");
        });

        builder.Metadata
            .FindNavigation(nameof(User.Sessions))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
