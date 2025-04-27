using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SalesController> _logger;

        public SalesController(ISaleRepository saleRepository, IMapper mapper, ILogger<SalesController> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSaleRequest request)
        {
            var sale = new Sale(request.SaleNumber, request.SaleDate, request.Customer, request.Branch);

            foreach (var item in request.Items)
            {
                sale.AddItem(item.ProductName, item.Quantity, item.UnitPrice);
            }

            await _saleRepository.AddAsync(sale);

            _logger.LogInformation($"[SaleCreatedEvent] Sale {sale.Id} created.");

            return CreatedAtAction(nameof(GetByIdAsync), new { id = sale.Id }, sale.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);

            if (sale == null)
                return NotFound();

            var response = _mapper.Map<GetSaleResponse>(sale);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateSaleRequest request)
        {
            var sale = await _saleRepository.GetByIdAsync(id);

            if (sale == null)
                return NotFound();

            sale.Items.Clear();

            foreach (var item in request.Items)
            {
                sale.AddItem(item.ProductName, item.Quantity, item.UnitPrice);
            }

            sale.RecalculateTotal();

            await _saleRepository.UpdateAsync(sale);

            _logger.LogInformation($"[SaleModifiedEvent] Sale {sale.Id} modified.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelAsync(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);

            if (sale == null)
                return NotFound();

            sale.Cancel();
            await _saleRepository.UpdateAsync(sale);

            _logger.LogInformation($"[SaleCancelledEvent] Sale {sale.Id} cancelled.");

            return NoContent();
        }
    }
}
