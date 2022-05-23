using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;

namespace VattaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly MongoCartsService _mongoCartsService;

        public CartsController(MongoCartsService mongoCartsService)
        {
            _mongoCartsService = mongoCartsService;
        }

        [HttpGet]
        public async Task<List<Cart>> Get() =>
            await _mongoCartsService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Cart>> Get(string id)
        {
            var cart = await _mongoCartsService.Get(id);

            if (cart is null)
            {
                return NotFound();
            }

            return cart;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cart newCart)
        {
            await _mongoCartsService.Create(newCart);

            return CreatedAtAction(nameof(Get), new { id = newCart.Id }, newCart);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Cart updateCart)
        {
            var cart = await _mongoCartsService.Get(id);

            if (cart is null)
            {
                return NotFound();
            }

            updateCart.Id = cart.Id;

            await _mongoCartsService.Update(id, updateCart);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var cart = await _mongoCartsService.Get(id);

            if (cart is null)
            {
                return NotFound();
            }

            await _mongoCartsService.Remove(id);

            return NoContent();
        }
    }

}
