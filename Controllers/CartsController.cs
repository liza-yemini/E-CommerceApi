using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartsService _cartsService;

        public CartsController(ICartsService cartsService)
        {
            _cartsService = cartsService;
        }

        [HttpGet]
        public async Task<List<Cart>> Get() =>
            await _cartsService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Cart>> Get(string id)
        {
            var cart = await _cartsService.Get(id);

            if (cart is null)
            {
                return NotFound();
            }

            return cart;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cart newCart)
        {
            await _cartsService.Create(newCart);

            return CreatedAtAction(nameof(Get), new { id = newCart.Id }, newCart);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Cart updateCart)
        {
            var cart = await _cartsService.Get(id);

            if (cart is null)
            {
                return NotFound();
            }

            updateCart.Id = cart.Id;

            await _cartsService.Update(id, updateCart);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var cart = await _cartsService.Get(id);

            if (cart is null)
            {
                return NotFound();
            }

            await _cartsService.Remove(id);

            return NoContent();
        }
    }

}
