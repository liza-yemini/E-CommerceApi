using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Controllers;

public class OrderedProductsController : Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderedProductService _orderedProductsService;

        public OrdersController(IOrderedProductService orderedProductsService)
        {
            _orderedProductsService = orderedProductsService;
        }

        [HttpGet]
        public async Task<List<OrderedProduct>> Get() =>
            await _orderedProductsService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<OrderedProduct>> Get(string id)
        {
            var product = await _orderedProductsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderedProduct newOrderedProduct)
        {
            await _orderedProductsService.Create(newOrderedProduct);

            return CreatedAtAction(nameof(Get), new { id = newOrderedProduct.Id }, newOrderedProduct);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, OrderedProduct updateOrderedProduct)
        {
            var product = await _orderedProductsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            updateOrderedProduct.Id = product.Id;

            await _orderedProductsService.Update(id, updateOrderedProduct);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _orderedProductsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            await _orderedProductsService.Remove(id);

            return NoContent();
        }
    }
}