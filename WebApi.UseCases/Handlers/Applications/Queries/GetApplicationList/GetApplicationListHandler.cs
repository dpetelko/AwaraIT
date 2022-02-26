using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Interfaces.DataAccess;

namespace WebApi.UseCases.Handlers.Applications.Queries.GetApplicationList;

/// <summary>
/// Обработчик запроса на получения списка Заявок
/// </summary>
public class GetApplicationListHandler : IRequestHandler<GetApplicationListRequest, IEnumerable<GetApplicationListResponse>>
{
    private readonly IDbContext _dbContext;

    public GetApplicationListHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetApplicationListResponse>> Handle(GetApplicationListRequest request, CancellationToken cancellationToken)
    {
        return await _dbContext.Applications
            .AsNoTracking()
            .Include(x => x.ApprovalProcess)
            .ThenInclude(x => x.Approver)
            .Select(x => new GetApplicationListResponse(
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