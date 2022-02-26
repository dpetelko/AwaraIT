using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities.Enums;
using WebApi.Entities.Models;
using WebApi.Infrastructure.Interfaces.DataAccess;
using WebApi.UseCases.Exceptions;

namespace WebApi.UseCases.Handlers.Applications.Commands.ApproveApplication;

/// <summary>
/// Обработчик запроса
/// </summary>
public class ApproveApplicationRequestHandler : AsyncRequestHandler<ApproveApplicationRequest>
{
    private readonly IDbContext _dbContext;

    public ApproveApplicationRequestHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task Handle(ApproveApplicationRequest request, CancellationToken cancellationToken)
    {
        var applicationId = request.ApplicationId;
        var userId = request.UserId;
        
        var application = await _dbContext.Applications
            .Include(x => x.ApprovalProcess)
            .ThenInclude(x => x.Approver)
            .SingleOrDefaultAsync(x => 
                x.Id == applicationId, cancellationToken);

        var applicationStatus = application.Status;
        
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => 
                x.Id == userId, cancellationToken);

        var userRole = user.Role;

        var approvedTask = application.ApprovalProcess
            .SingleOrDefault(x => x.Role == userRole);
        
        Validate(approvedTask, applicationStatus);
        
        var isApproved = request.IsApproved;
        var comment = request.Comment;

        approvedTask.Approve(userId, isApproved, comment);




    }

    private static void Validate(ApprovalTask task, ApplicationStatus status)
    {
        if (task == null)
        {
            throw new ValidationException($"Роль Пользователя не указана в качестве согласующего.");
        }
        
        if (status != ApplicationStatus.OnApproval)
        {
            throw new ValidationException($"Статус заявки не позвляет проводить");
        }
        
        if (task.ApproverId != null)
        {
            throw new ValidationException($"Заявка была согласована {task.ApprovalTimestamp.Value}");
        }
    }
}