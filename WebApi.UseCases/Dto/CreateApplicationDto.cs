using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WebApi.Entities.Enums;

namespace WebApi.UseCases.Dto;

/// <summary>
/// Структура данных для создания заявки
/// </summary>
public class CreateApplicationDto
{
    [JsonConstructor]
    public CreateApplicationDto(
        string name,
        string description,
        ApplicationPriority priority,
        DateTime executionDate,
        IEnumerable<Role> approvalProcessRoles)
    {
        Name = name;
        Description = description;
        Priority = priority;
        ExecutionDate = executionDate;
        ApprovalProcessRoles = approvalProcessRoles;
    }

    /// <summary> Наименование заявки </summary>
    public string Name { get; }
    
    /// <summary> Описание заявки </summary>
    public string Description { get; }
    
    /// <summary> Приоритет </summary>
    public ApplicationPriority Priority { get; }
    
    /// <summary> Дата исполнения </summary>
    public DateTime ExecutionDate { get; }
    
    /// <summary> Список ролей процесса согласования </summary>
    public IEnumerable<Role> ApprovalProcessRoles { get; }
}