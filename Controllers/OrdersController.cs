using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<List<Order>> Get() =>
            await _ordersService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Order>> Get(string id)
        {
            var order = await _ordersService.Get(id);

            if (order is null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order newOrder)
        {
            await _ordersService.Create(newOrder);

            return CreatedAtAction(nameof(Get), new { id = newOrder.Id }, newOrder);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Order updateOrder)
        {
            var order = await _ordersService.Get(id);

            if (order is null)
            {
                return NotFound();
            }

            updateOrder.Id = order.Id;

            await _ordersService.Update(id, updateOrder);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await _ordersService.Get(id);

            if (order is null)
            {
                return NotFound();
            }

            await _ordersService.Remove(id);

            return NoContent();
        }
    }
}
