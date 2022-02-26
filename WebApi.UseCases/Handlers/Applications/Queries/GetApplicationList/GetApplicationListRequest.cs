using System.Collections;
using System.Collections.Generic;
using MediatR;

namespace WebApi.UseCases.Handlers.Applications.Queries.GetApplicationList;

/// <summary>
/// Структура данных запроса для получения Списка заявок
/// </summary>
public class GetApplicationListRequest : IRequest<IEnumerable<GetApplicationListResponse>>
{
}