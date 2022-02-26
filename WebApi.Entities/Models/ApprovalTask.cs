using System;
using System.Linq;
using WebApi.Entities.Enums;

namespace WebApi.Entities.Models;

/// <summary>
/// Сущность шаг процесса согласования
/// </summary>
public class ApprovalTask : Entity
{
    public ApprovalTask() { }

    public ApprovalTask(Role role)
    {
        Role = role;
    }

    /// <summary> Роль </summary>
    public Role Role { get; private set; }
    
    /// <summary> Таймштамп согласования </summary>
    public DateTime? ApprovalTimestamp { get; private set; }
    
    /// <summary> Флаг согласования </summary>
    public bool IsApproved { get; private set; }
    
    /// <summary> FK на сущность согласующего </summary>
    public Guid? ApproverId { get; private set; }
    
    /// <summary> Сущность Соглалующий </summary>
    public User Approver { get; private set; }

    /// <summary> Комментарий согласующего </summary>
    public string Comment { get; private set; }

    /// <summary> FK заявки </summary>
    public Guid ApplicationId { get; private set; }
    
    /// <summary> Сущность заявки </summary>
    public Application Application { get; private set; }

    /// <summary>
    /// Устанавливает статус согласования
    /// </summary>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="isApproved">Флаг согласования</param>
    /// <param name="comment">Комментарий согласующего</param>
    public void Approve(Guid userId, bool isApproved, string comment)
    {
        Comment = comment;
        IsApproved = isApproved;
        ApprovalTimestamp = DateTime.Now;
        ApproverId = userId;
        if (!isApproved)
        {
            Application.SetStatus(ApplicationStatus.Rejected);
        }
        else
        {
            if (Application.ApprovalProcess.All(x => x.IsApproved))
            {
                Application.SetStatus(ApplicationStatus.Approved);
            }
        }
    }
}