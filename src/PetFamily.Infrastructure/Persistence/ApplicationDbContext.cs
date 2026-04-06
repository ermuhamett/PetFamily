using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Volunteer> Volunteers => Set<Volunteer>();
    public DbSet<Pet> Pets => Set<Pet>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
