using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebApi.Entities.Enums;

namespace WebApi.UseCases.Handlers.Applications.Queries.GetApplicationList;

/// <summary>
/// Структура данных для Списка заявок
/// </summary>
public class GetApplicationListResponse
{

    [JsonConstructor]
    public GetApplicationListResponse(
        string name,
        string description,
        ApplicationPriority priority,
        DateTime executionDate, 
        IEnumerable<ApprovalTaskDto> approvalProcess,
        ApplicationStatus status,
        DateTime? approvalStartDate = null)
    {
        Name = name;
        Description = description;
        Priority = priority;
        ExecutionDate = executionDate;
        ApprovalProcess = approvalProcess;
        Status = status;
        ApprovalStartDate = approvalStartDate;
    }
 
    /// <summary> Наименование заявки </summary>
    public string Name { get; private set; }
    
    /// <summary> Описание заявки </summary>
    public string Description { get; private set; }
    
    /// <summary> Приоритет заявки </summary>
    public ApplicationPriority Priority { get; private set; }
    
    /// <summary> Дата исполнения </summary>
    public DateTime ExecutionDate { get; private set; }
    
    /// <summary> Описание процесса согласования </summary>
    public IEnumerable<ApprovalTaskDto> ApprovalProcess { get; private set; }
    
    /// <summary> Статус заявки </summary>
    public ApplicationStatus Status { get; private set; }
    
    /// <summary> Дата направления на согласование </summary>
    public DateTime? ApprovalStartDate { get; private set; }
}

/// <summary>
/// Структура данных для описания процесса согласования
/// </summary>
public class ApprovalTaskDto
{
    [JsonConstructor]
    public ApprovalTaskDto(Role role,
        bool isApproved,
        string approverName,
        string comment,
        DateTime? approvalTimestamp = null)
    {
        Role = role;
        IsApproved = isApproved;
        ApprovalTimestamp = approvalTimestamp;
        ApproverName = approverName;
        Comment = comment;
    }

    /// <summary> Роль в процессе согласования </summary>
    public Role Role { get; private set; }
    
    /// <summary> Флаг согласования </summary>
    public bool IsApproved { get; private set; }
    
    /// <summary> Таймштамп согласования </summary>
    public DateTime? ApprovalTimestamp { get; private set; }
    
    /// <summary> Имя согласователя </summary>
    public string ApproverName { get; private set; }
    
    /// <summary> Комментарий согласующего </summary>
    public string Comment { get; private set; }
}