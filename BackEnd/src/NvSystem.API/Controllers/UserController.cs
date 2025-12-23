using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Application.UseCases.User.Commands;
using NvSystem.Application.UseCases.User.Query;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Entities;

namespace NvSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public UserController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new {id = userId}, userId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var entity = await _mediator.Send(new GetByIdQuery(id), cancellationToken);
            return Ok(entity);
        }

        /*[HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest user)
        {
            var result = await _userService.CreateUser(user);
            return Ok(result);
        }*/
    }
}
