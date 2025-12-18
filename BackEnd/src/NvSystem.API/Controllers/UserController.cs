using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Application.UseCases.User.Commands;
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

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        /*[HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest user)
        {
            var result = await _userService.CreateUser(user);
            return Ok(result);
        }*/
    }
}
