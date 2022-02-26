using System;
using MediatR;
using WebApi.UseCases.Validation;

namespace WebApi.UseCases.Handlers.Applications.Commands.StartApplicationApprovalProcess;

/// <summary>
/// Структура данных для команды старта согласования Заявки
/// </summary>
public class StartApplicationApprovalProcessRequest : IRequest
{
    public StartApplicationApprovalProcessRequest(Guid applicationId, Guid userId)
    {
        ApplicationId = applicationId;
        UserId = userId;
    }

    /// <summary>
    /// Идентификатор заявки
    /// </summary>
    [NotDefault]
    public Guid ApplicationId { get; private set; }
    
    /// <summary> Роль Пользователя </summary>
    [NotDefault]
    public Guid UserId { get; private set; }
}