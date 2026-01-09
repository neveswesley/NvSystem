using MediatR;
using Microsoft.AspNetCore.Mvc;
using NvSystem.Application.UseCases.Sale.Commands;
using NvSystem.Application.UseCases.Sale.Queries;
using NvSystem.Application.UseCases.SaleItem.Commands;
using NvSystem.Application.UseCases.SaleItem.Queries;

namespace NvSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/CreateSale")]
        public async Task<IActionResult> Post(
            CancellationToken cancellationToken)
        {
            var sale = await _mediator.Send(new CreateSaleCommand(), cancellationToken);
            return Ok(sale);
        }

        [HttpPost("/CreateSaleItem")]
        public async Task<IActionResult> AddItem([FromQuery] Guid saleId, [FromQuery] Guid productId, [FromQuery] int quantity, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new AddSaleItemCommand(saleId, productId, quantity), cancellationToken);
            return CreatedAtAction(nameof(GetSaleItemById), new {id = saleId}, response);
        }

        [HttpGet("/GetSaleItem/{id}")]
        public async Task<IActionResult> GetSaleItemById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var saleItem = await _mediator.Send(new GetSaleItemByIdQuery(id), cancellationToken);
            return Ok(saleItem);
        }

        [HttpGet("/GetSaleById/{id}")]
        public async Task<IActionResult> GetSaleById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var sale = await _mediator.Send(new GetSaleByIdQuery(id), cancellationToken);
            return Ok(sale);
        }

        [HttpGet("/GetSaleItems")]
        public async Task<IActionResult> GetSaleItems(CancellationToken cancellationToken, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetSalesQuery
            {
                Page = page,
                PageSize = pageSize
            }, cancellationToken);
            
            return Ok(result);
        }
    }
}