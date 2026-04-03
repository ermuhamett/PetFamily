using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Persistence;

public sealed class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Pet> Pets => Set<Pet>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Database"));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}
