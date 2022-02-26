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

namespace WebApi.UseCases.Handlers.Applications.Commands.CreateApplication;

/// <summary>
/// Класс обработки запроса на создание заявки
/// </summary>
public class CreateApplicationRequestHandler : AsyncRequestHandler<CreateApplicationRequest>
{
    private readonly IDbContext _dbContext;

    public CreateApplicationRequestHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task Handle(CreateApplicationRequest request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);
        var entity = new Application(
            request.Name,
            request.Description,
            request.ExecutionDate,
            request.Priority,
            request.ApprovalProcessRoles.Select(x => new ApprovalTask(x)),
            request.UserId);
        await _dbContext.Applications.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task Validate(CreateApplicationRequest request, CancellationToken cancellationToken)
    {
        CreateApplicationRequestValidator validator = new();
        var result = await validator.ValidateAsync(request, cancellationToken);
        if (result.IsValid) return;
        
        Dictionary<string, List<string>> validationMsg = new();
        foreach (var error in result.Errors)
        {
            var propertyName = error.PropertyName;
            var errorName = error.ErrorMessage;
            if (validationMsg.ContainsKey(propertyName))
            {
                validationMsg[errorName].Add(errorName);
            }
            else
            {
                validationMsg.Add(error.PropertyName, new List<string>{error.ErrorMessage});
            }
        }
       
        throw new ValidationException(validationMsg);
    }
    
    
}