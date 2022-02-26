using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities.Enums;
using WebApi.Entities.Models;
using WebApi.Infrastructure.Interfaces.DataAccess;
using WebApi.UseCases.Handlers.Applications.Queries.GetApplicationList;

namespace WebApi.UseCases.Handlers.Applications.Queries.GetApplicationListForApprove;

public class GetApplicationListForApproveHandler : IRequestHandler<GetApplicationListForApproveRequest, IEnumerable<GetApplicationListForApproveResponse>>
{
    private readonly IDbContext _dbContext;

    public GetApplicationListForApproveHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetApplicationListForApproveResponse>> Handle(GetApplicationListForApproveRequest request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var role = await _dbContext.Users
            .Where(x => x.Id == userId)
            .Select(x => x.Role)
            .SingleOrDefaultAsync(cancellationToken);
        
        IQueryable<Application> applicationList = _dbContext.Applications
            .AsNoTracking()
            .Include(x => x.ApprovalProcess)
            .ThenInclude(x => x.Approver);
        
        switch (role)
        {
            case Role.Clerk:
                applicationList = applicationList.Where(x => x.CreatorId == userId);
                break;
            case Role.Initiator:
                applicationList = applicationList.Where(x => x.InitiatorId == userId);
                break;
            case Role.Accountant or Role.Chief or Role.Manager:
                applicationList = applicationList.Where(x => x.ApprovalProcess.Any(x => x.Role == role));
                break;
        }
        return await applicationList.Select(x => new GetApplicationListForApproveResponse(
                x.Name,
                x.Description,
                x.Priority,
                x.ExecutionDate,
                x.ApprovalProcess
                    .Select(y =>
                        new ApprovalTaskDto(
                            y.Role,
                            y.IsApproved,
                            y.Approver.Name,
                            y.Comment,
                            y.ApprovalTimestamp))
                    .ToList(),
                x.Status,
                x.ApprovalStartDate))
            .ToListAsync(cancellationToken);
    }
}