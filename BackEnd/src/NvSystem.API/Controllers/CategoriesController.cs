using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NvSystem.Application.UseCases.Category.Commands;
using NvSystem.Application.UseCases.Category.Query;
using NvSystem.Domain.DTOs;
using NvSystem.Domain.Entities;

namespace NvSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var categoryId = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new {id = categoryId}, new {id = categoryId});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromRoute] Guid id, UpdateCategoryDto dto,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new UpdateCategoryCommand(id, dto), cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpPatch("DisableCategory/{id}")]
        public async Task<ActionResult> DisableCategory([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DisableCategoryCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult> GetAllCategories(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllCategoryQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetAllActiveCategories")]
        public async Task<ActionResult> GetAllActiveCategories(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllActiveCategoriesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id), cancellationToken);
            return Ok(result);
        }
    }
}