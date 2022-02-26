using System;
using System.Collections.Generic;
using MediatR;
using WebApi.Entities.Enums;
using WebApi.UseCases.Validation;

namespace WebApi.UseCases.Handlers.Applications.Queries.GetApplicationListForApprove;

/// <summary>
/// Структура данных для запроса получения списка заявок на согласование
/// </summary>
public class GetApplicationListForApproveRequest : IRequest<IEnumerable<GetApplicationListForApproveResponse>>
{
    public GetApplicationListForApproveRequest(Guid userId)
    {
        UserId = userId;
    }
    
    /// <summary> Роль Пользователя </summary>
    [NotDefault]
    public Guid UserId { get; private set; }
}