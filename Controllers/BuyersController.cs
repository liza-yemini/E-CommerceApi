using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;

namespace VattaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuyersController : ControllerBase
    {
        private readonly MongoBuyersService _mongoBuyersService;

        public BuyersController(MongoBuyersService mongoBuyersService)
        {
            _mongoBuyersService = mongoBuyersService;
        }

        [HttpGet]
        public async Task<List<Buyer>> Get() =>
            await _mongoBuyersService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Buyer>> Get(string id)
        {
            var client = await _mongoBuyersService.Get(id);

            if (client is null)
            {
                return NotFound();
            }

            return client;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Buyer newBuyer)
        {
            Random rnd = new Random();
            int year = rnd.Next(1950, 2005);
            int month = rnd.Next(1, 13);
            int day = rnd.Next(1, 28);
            newBuyer.Birthday = new DateTime(year, month, day);
            if (newBuyer.Birthday != null)
            {

                int age = DateTime.Now.Year - newBuyer.Birthday.Year;
                if (DateTime.Now.Month < newBuyer.Birthday.Month || (DateTime.Now.Month < newBuyer.Birthday.Month &&
                                                                      DateTime.Now.Day < newBuyer.Birthday.Day))
                {
                    age--;
                }

                newBuyer.Age = age;
            }
            await _mongoBuyersService.Create(newBuyer);

            return CreatedAtAction(nameof(Get), new { id = newBuyer.Id }, newBuyer);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Buyer updateBuyer)
        {
            var buyer = await _mongoBuyersService.Get(id);

            if (buyer is null)
            {
                return NotFound();
            }

            updateBuyer.Id = buyer.Id;

            await _mongoBuyersService.Update(id, updateBuyer);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var client = await _mongoBuyersService.Get(id);

            if (client is null)
            {
                return NotFound();
            }

            await _mongoBuyersService.Remove(id);

            return NoContent();
        }
    }
}
