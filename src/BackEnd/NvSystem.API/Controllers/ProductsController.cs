using MediatR;
using Microsoft.AspNetCore.Mvc;
using NvSystem.Application.UseCases.Product.Commands;
using NvSystem.Application.UseCases.Product.Queries;

namespace NvSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new {id = result}, result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetAllProductsQuery(), ct);
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id), ct);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteProductCommand(id), ct);
            return NoContent();
        }

        [HttpPatch("/DisableProduct/{id}")]
        public async Task<IActionResult> DisableProduct([FromRoute] Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DisableProductCommand(id), ct);
            return NoContent();
        }

        [HttpPatch("/EnableProduct/{id}")]
        public async Task<IActionResult> EnableProduct([FromRoute] Guid id, CancellationToken ct)
        {
            //await _mediator.Send(new ActiveProductCommand(id), ct);
            return NoContent();
        }

        [HttpGet("/GetLookupProduct/{barcode}")]
        public async Task<IActionResult> GetLookupProduct([FromRoute] string barcode, CancellationToken ct)
        {
            var product = await _mediator.Send(new GetLookupByBarcode(barcode));
            return Ok(product);
        }
        
    }
}
