using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;

namespace VattaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<List<Product>> Get() =>
            await _productsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productsService.GetAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product newProduct)
        {
            await _productsService.CreateAsync(newProduct);

            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product updateProduct)
        {
            var product = await _productsService.GetAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            updateProduct.Id = product.Id;

            await _productsService.UpdateAsync(id, updateProduct);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productsService.GetAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            await _productsService.RemoveAsync(id);

            return NoContent();
        }



    }
}
