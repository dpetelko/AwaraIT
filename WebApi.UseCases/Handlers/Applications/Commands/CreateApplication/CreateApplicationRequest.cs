using System;
using System.Collections.Generic;
using MediatR;
using Newtonsoft.Json;
using WebApi.Entities.Enums;

namespace WebApi.UseCases.Handlers.Applications.Commands.CreateApplication;

/// <summary>
/// Структура данных для запроса Создания заявки
/// </summary>
public class CreateApplicationRequest : IRequest
{
    public CreateApplicationRequest(
        string name,
        string description,
        ApplicationPriority priority,
        DateTime executionDate,
        IEnumerable<Role> approvalProcessRoles,
        Guid userId)
    {
        Name = name;
        Description = description;
        Priority = priority;
        ExecutionDate = executionDate;
        ApprovalProcessRoles = approvalProcessRoles;
        UserId = userId;
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
    
    /// <summary> Список ролей процесса согласования </summary>
    public Guid UserId { get; }
}