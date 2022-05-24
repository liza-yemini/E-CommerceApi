using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<List<Product>> Get() =>
            await _productsService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("category/{category}")]
        public async Task<List<Product>> GetByCategory(string category) =>
            await _productsService.GetByCategory(category);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product newOrderedProduct)
        {
            await _productsService.Create(newOrderedProduct);

            return CreatedAtAction(nameof(Get), new { id = newOrderedProduct.Id }, newOrderedProduct);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product updateOrderedProduct)
        {
            var product = await _productsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            updateOrderedProduct.Id = product.Id;

            await _productsService.Update(id, updateOrderedProduct);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productsService.Get(id);

            if (product is null)
            {
                return NotFound();
            }

            await _productsService.Remove(id);

            return NoContent();
        }
    }
}
