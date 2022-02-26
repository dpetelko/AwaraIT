using Microsoft.EntityFrameworkCore;
using WebApi.Entities.Models;
using WebApi.Infrastructure.Interfaces.DataAccess;

namespace WebApi.DataAccess.MsSql;

public class AppDbContext : DbContext, IDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<ApprovalTask> ApprovalTasks { get; set; }
    public DbSet<Application> Applications { get; set; }
}