using MediatR;
using Microsoft.AspNetCore.Mvc;
using NvSystem.Application.Services.Interfaces;
using NvSystem.Application.UseCases.User;
using NvSystem.Application.UseCases.User.Query;
using NvSystem.Communications.Requests;

namespace NvSystem.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RegisterUserUseCase _registerUserUseCase;

        public UsersController(IMediator mediator, RegisterUserUseCase registerUserUseCase)
        {
            _mediator = mediator;
            _registerUserUseCase = registerUserUseCase;
        }


        /*[HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new {id = userId}, userId);
        }*/
        
        [HttpPost]
        public async Task<IActionResult> Register(RequestRegisterUserJson request)
        {
            var result = await _registerUserUseCase.Execute(request);
            return Ok(result);
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
