using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities.Models;

namespace WebApi.Infrastructure.Interfaces.DataAccess;

public interface IDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<ApprovalTask> ApprovalTasks { get; set; }
    DbSet<Application> Applications { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}