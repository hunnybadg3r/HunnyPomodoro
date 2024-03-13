using HunnyPomodoro.Domain.Session;
using HunnyPomodoro.Domain.Session.ValueObjects;
using HunnyPomodoro.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HunnyPomodoro.Infrastructure.Persistence.Configurations;

public class SessionConfigurations : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
        .ValueGeneratedNever()
        .HasConversion(
            id => id.Value, 
            value => SessionId.Create(value));

        builder.Property(s => s.Status)
            .HasMaxLength(20);

        builder.Property(s => s.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
    }
}