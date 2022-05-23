using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;

namespace VattaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly MongoProductsService _mongoProductsService;

        public ProductsController(MongoProductsService mongoProductsService)
        {
            _mongoProductsService = mongoProductsService;
        }

        [HttpGet]
        public async Task<List<Product>> Get() =>
            await _mongoProductsService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _mongoProductsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("category/{category}")]
        public async Task<List<Product>> GetByCategory(string category) =>
            await _mongoProductsService.GetByCategory(category);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product newOrderedProduct)
        {
            await _mongoProductsService.Create(newOrderedProduct);

            return CreatedAtAction(nameof(Get), new { id = newOrderedProduct.Id }, newOrderedProduct);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product updateOrderedProduct)
        {
            var product = await _mongoProductsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            updateOrderedProduct.Id = product.Id;

            await _mongoProductsService.Update(id, updateOrderedProduct);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _mongoProductsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            await _mongoProductsService.Remove(id);

            return NoContent();
        }
    }
}
