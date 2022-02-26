using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApi.Entities.Enums;
using WebApi.Infrastructure.Interfaces.DataAccess;
using WebApi.UseCases.Dto;
using WebApi.UseCases.Exceptions;
using WebApi.UseCases.Handlers.Applications.Commands.ApproveApplication;
using WebApi.UseCases.Handlers.Applications.Commands.CreateApplication;
using WebApi.UseCases.Handlers.Applications.Commands.StartApplicationApprovalProcess;
using WebApi.UseCases.Handlers.Applications.Queries.GetApplicationList;
using WebApi.UseCases.Handlers.Applications.Queries.GetApplicationListForApprove;
using static Newtonsoft.Json.Formatting;

namespace WebApi.WebApi.Controllers
{
    /// <summary>
    /// Контроллер по работе с заявками
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly ILogger<ApplicationsController> _logger;
        private readonly IMediator _mediator;

        public ApplicationsController(
            ILogger<ApplicationsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Создать заявку
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateApplication([FromBody] CreateApplicationDto dto)
        {
            _logger.LogInformation($"Создать заявку {Environment.NewLine}{JsonConvert.SerializeObject(dto, Indented)}");
            var userId = Guid.NewGuid(); // todo заменить на получение userId из HttpContext
            await _mediator.Send(
                new CreateApplicationRequest(
                    dto.Name,
                    dto.Description,
                    dto.Priority,
                    dto.ExecutionDate,
                    dto.ApprovalProcessRoles,
                    userId));
            return Ok();
        }
        
        /// <summary>
        /// Получить полный список заявок
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetApplicationList()
        {
            _logger.LogInformation("Получить полный список заявок");
            var result = await _mediator.Send(new GetApplicationListRequest());
            return Ok(result);
        }
        
        /// <summary>
        /// Получить Cписок заявок для Пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("approve")]
        public async Task<ActionResult> GetApplicationListForApprove()
        {
            _logger.LogInformation("Получить Cписок заявок для Пользователя");
            var userId = Guid.NewGuid(); // todo заменить на получение userId из HttpContext
            var result = await _mediator.Send(new GetApplicationListForApproveRequest(userId));
            return Ok(result);
        }
        
        /// <summary>
        /// Инициирование согласования заявки
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [HttpPut("initial/{applicationId:guid}")]
        public async Task<ActionResult> StartApplicationApprovalProcess(Guid applicationId)
        {
            _logger.LogInformation($"Инициирование согласования заявки ID = '{applicationId}'");
            var userId = Guid.NewGuid(); // todo заменить на получение userId из HttpContext
            await _mediator.Send(new StartApplicationApprovalProcessRequest(applicationId, userId));
            return Ok();
        }
        
        /// <summary>
        /// Согласование заявки
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("approove")]
        public async Task<ActionResult> ApproveApplication([FromBody] ApproveApplicationDto dto)
        {
            _logger.LogInformation($"Согласование заявки {Environment.NewLine}{JsonConvert.SerializeObject(dto, Indented)}");
            var userId = Guid.NewGuid(); // todo заменить на получение userId из HttpContext
            await _mediator.Send(new ApproveApplicationRequest(
                dto.ApplicationId,
                userId,
                dto.IsApproved,
                dto.Comment));
            return Ok();
        }
    }
}
