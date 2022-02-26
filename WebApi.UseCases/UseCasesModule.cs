
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using WebApi.UseCases.Handlers.Applications.Commands.ApproveApplication;
using WebApi.UseCases.Handlers.Applications.Commands.CreateApplication;
using WebApi.UseCases.Handlers.Applications.Commands.StartApplicationApprovalProcess;
using WebApi.UseCases.Handlers.Applications.Queries.GetApplicationList;
using WebApi.UseCases.Handlers.Applications.Queries.GetApplicationListForApprove;
using WebApi.Utils.Modules;

namespace WebApi.UseCases;

/// <summary>
/// Регистратор Юскейсов
/// </summary>
public class UseCasesModule : Module
{
    public override void Load(IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateApplicationRequestHandler));
        services.AddMediatR(typeof(GetApplicationListHandler));
        services.AddMediatR(typeof(StartApplicationApprovalProcessRequestHandler));
        services.AddMediatR(typeof(GetApplicationListForApproveHandler));
        services.AddMediatR(typeof(ApproveApplicationRequestHandler));
    }
}