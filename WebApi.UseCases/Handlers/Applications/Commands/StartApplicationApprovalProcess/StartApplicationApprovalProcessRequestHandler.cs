using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities.Enums;
using WebApi.Entities.Models;
using WebApi.Infrastructure.Interfaces.DataAccess;
using WebApi.UseCases.Exceptions;

namespace WebApi.UseCases.Handlers.Applications.Commands.StartApplicationApprovalProcess;

/// <summary>
/// Обработчик запроса иницирования 
/// </summary>
public class StartApplicationApprovalProcessRequestHandler : AsyncRequestHandler<StartApplicationApprovalProcessRequest>
{
    private readonly IDbContext _dbContext;

    public StartApplicationApprovalProcessRequestHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task Handle(StartApplicationApprovalProcessRequest request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Applications
            .SingleOrDefaultAsync(x =>
                    x.Id == request.ApplicationId,
                cancellationToken);
        Validate(entity);
        entity.StartApproval(DateTime.Now, request.UserId);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static void Validate(Application entity)
    {
        Dictionary<string, List<string>> validationResult = new();
        if (entity == null) throw new EntityNotFoundException("Заявка не найдена.");
        if (entity.Status != ApplicationStatus.Draft)
        {
            validationResult.Add("Статус заявки",
                new List<string>
                {
                    "Инициирование согласования возможно только для статуса Черновик."
                });
        }
        
        if (entity.ExecutionDate < DateTime.Now.Date)
        {
            validationResult.Add("Дата исполнения",
                new List<string>
                {
                    "Время для выполнения заявки истекло."
                });
        }

        if (validationResult.Any())
        {
            throw new ValidationException(validationResult);
        }
    }
}