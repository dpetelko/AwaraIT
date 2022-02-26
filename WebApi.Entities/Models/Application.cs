using System;
using System.Collections;
using System.Collections.Generic;
using WebApi.Entities.Enums;

namespace WebApi.Entities.Models;

/// <summary>
/// Сущность Заявка
/// </summary>
public class Application : Entity
{
    protected Application(Guid creatorId)
    {
        CreatorId = creatorId;
    }

    public Application(
        string name,
        string description,
        DateTime executionDate,
        ApplicationPriority priority,
        IEnumerable<ApprovalTask> approvalProcess, Guid creatorId, 
        DateTime? approvalStartDate = null,
        ApplicationStatus status = ApplicationStatus.Draft)
    {
        Name = name;
        Description = description;
        ExecutionDate = executionDate;
        Priority = priority;
        Status = status;
        ApprovalStartDate = approvalStartDate;
        ApprovalProcess = approvalProcess;
        CreatorId = creatorId;
    }

    /// <summary> Ниманование заявки </summary>
    public string Name { get; private set; }
    
    /// <summary> Описание заявки </summary>
    public string Description { get; private set; }
    
    /// <summary> Дата выполнения </summary>
    public DateTime ExecutionDate { get; private set; }
    
    /// <summary> Приоритет заявки </summary>
    public ApplicationPriority Priority { get; private set; }
    
    /// <summary> Статус заявки </summary>
    public ApplicationStatus Status { get; private set; }
    
    /// <summary> Дата направления на согласование </summary>
    public DateTime? ApprovalStartDate { get; private set; }
    
    /// <summary> Список элеметнотов процесса согласования </summary>
    public IEnumerable<ApprovalTask> ApprovalProcess { get; private set; }
    
    /// <summary> FK на Создателя </summary>
    public Guid CreatorId { get; private set; }
    
    /// <summary> Создатель </summary>
    public User Creator { get; private set; }
    
    /// <summary> FK на Инициатора </summary>
    public Guid? InitiatorId { get; private set; }
    
    /// <summary> Инициатор </summary>
    public User Initiator { get; private set; }

    /// <summary>
    /// Инициирует процесс согласования
    /// </summary>
    /// <param name="startTimestamp">Таймштамп начала согласования</param>
    /// <param name="initiatorId"></param>
    public void StartApproval(DateTime startTimestamp, Guid initiatorId)
    {
        ApprovalStartDate = startTimestamp;
        Status = ApplicationStatus.OnApproval;
        InitiatorId = initiatorId;
    }

    /// <summary>
    /// Установить статус заявки
    /// </summary>
    /// <param name="status">Устанавливаемый статус заявки</param>
    public void SetStatus(ApplicationStatus status)
    {
        Status = status;
    }
}