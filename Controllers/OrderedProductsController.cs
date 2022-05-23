using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;

namespace VattaAppApi.Controllers;

public class OrderedProductsController : Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly MongoOrderedProductsService _mongoOrderedProductsService;

        public OrdersController(MongoOrderedProductsService mongoOrderedProductsService)
        {
            _mongoOrderedProductsService = mongoOrderedProductsService;
        }

        [HttpGet]
        public async Task<List<OrderedProduct>> Get() =>
            await _mongoOrderedProductsService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<OrderedProduct>> Get(string id)
        {
            var product = await _mongoOrderedProductsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderedProduct newOrderedProduct)
        {
            await _mongoOrderedProductsService.Create(newOrderedProduct);

            return CreatedAtAction(nameof(Get), new { id = newOrderedProduct.Id }, newOrderedProduct);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, OrderedProduct updateOrderedProduct)
        {
            var product = await _mongoOrderedProductsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            updateOrderedProduct.Id = product.Id;

            await _mongoOrderedProductsService.Update(id, updateOrderedProduct);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _mongoOrderedProductsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            await _mongoOrderedProductsService.Remove(id);

            return NoContent();
        }
    }
}