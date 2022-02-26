using System;
using System.Linq;
using FluentValidation;
using WebApi.Entities.Enums;

namespace WebApi.UseCases.Handlers.Applications.Commands.CreateApplication;

/// <summary>
/// Валидатор соблюдения бизнес правил при создании Заявки
/// </summary>
public class CreateApplicationRequestValidator : AbstractValidator<CreateApplicationRequest>
{
    public CreateApplicationRequestValidator()
    {
        RuleFor(request => request.ApprovalProcessRoles)
            .Must(x => x.Count() > 1)
            .WithMessage("В согласовании должно участвовать несколько ролей.");
        
        RuleFor(request => request.ApprovalProcessRoles)
            .Must(x => x.Any(y => y == Role.Chief))
            .WithMessage("Отсутствует роль Руководитель.");
        
        RuleFor(request => request.ApprovalProcessRoles)
            .Must(x => x.All(y => y != Role.Clerk))
            .WithMessage("Применение роли Клерк в качестве согласующего не допускается.");
        
        RuleFor(request => request.ApprovalProcessRoles)
            .Must(x => x.All(y => y != Role.Initiator))
            .WithMessage("Применение роли Инициатор в качестве согласующего не допускается.");
        
        RuleFor(request => request.ExecutionDate)
            .Must(x => x.Date >= DateTime.Now.Date)
            .WithMessage("Заявки принимаются только на следующий день.");
    }
}