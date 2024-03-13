using HunnyPomodoro.Domain.Common.Models;
using HunnyPomodoro.Domain.Session;
using HunnyPomodoro.Domain.User;
using HunnyPomodoro.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace HunnyPomodoro.Infrastructure.Persistence;

public sealed class HunnyPomodoroDbContext : DbContext
{

    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public HunnyPomodoroDbContext(
        DbContextOptions<HunnyPomodoroDbContext> options
, PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(HunnyPomodoroDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .AddInterceptors(_publishDomainEventsInterceptor);

        base.OnConfiguring(optionsBuilder);
    }
}