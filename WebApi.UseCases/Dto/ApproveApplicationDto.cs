using System;
using WebApi.UseCases.Validation;

namespace WebApi.UseCases.Dto;

public class ApproveApplicationDto
{
    public ApproveApplicationDto(
        Guid applicationId,
        Guid userId,
        bool isApproved,
        string comment)
    {
        ApplicationId = applicationId;
        IsApproved = isApproved;
        Comment = comment;
    }

    /// <summary> Идентификатор заявки </summary>
    [NotDefault]
    public Guid ApplicationId { get; private set; }

    /// <summary> Решение по заявке </summary>
    public bool IsApproved { get; private set; }

    /// <summary> Комментарий согласующего </summary>
    public string Comment { get; private set; }
}