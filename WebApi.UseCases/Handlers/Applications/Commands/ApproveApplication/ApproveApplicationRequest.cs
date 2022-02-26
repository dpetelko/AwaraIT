using System;
using MediatR;
using Newtonsoft.Json;
using WebApi.UseCases.Validation;

namespace WebApi.UseCases.Handlers.Applications.Commands.ApproveApplication;

/// <summary>
/// Структура данных
/// </summary>
public class ApproveApplicationRequest : IRequest
{
    public ApproveApplicationRequest(
        Guid applicationId,
        Guid userId,
        bool isApproved,
        string comment)
    {
        ApplicationId = applicationId;
        UserId = userId;
        IsApproved = isApproved;
        Comment = comment;
    }

    /// <summary> Идентификатор заявки </summary>
    [NotDefault]
    public Guid ApplicationId { get; private set; }
    
    /// <summary> Идентификатор Пользователя </summary>
    [NotDefault]
    public Guid UserId { get; private set; }
    
    /// <summary> Решение по заявке </summary>
    public bool IsApproved { get; private set; }

    /// <summary> Комментарий согласующего </summary>
    public string Comment { get; private set; }

}