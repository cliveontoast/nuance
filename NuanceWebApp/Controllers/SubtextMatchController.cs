using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuanceWebApp.Dto;
using Serilog;

namespace NuanceWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubtextMatchController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public SubtextMatchController(
            ILogger logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string text, string subText)
        {
            _logger.Debug("Received {Controller} {Name} {Text} - {SubText}",
                nameof(SubtextMatchController),
                nameof(Get),
                text,
                subText);
            var result = await _mediator.Send(new MatchesCommand
            {
                Subtext = subText,
                Text = text
            });
            var response = new MatchesDto(result);
             _logger.Debug("Response {Controller} {Name} {Text} - {SubText} {@Response}",
                nameof(SubtextMatchController),
                nameof(Get),
                text,
                subText);
            return Ok(response);
        }
    }
}
