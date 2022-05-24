using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellersController : ControllerBase
    {
        private readonly ISellersService _sellersService;

        public SellersController(ISellersService sellersService)
        {
            _sellersService = sellersService;
        }

        [HttpGet]
        public async Task<List<Seller>> Get() =>
            await _sellersService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Seller>> Get(string id)
        {
            var seller = await _sellersService.Get(id);

            if (seller is null)
            {
                return NotFound();
            }

            return seller;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Seller newSeller)
        {
            Random rnd = new Random();
            int year = rnd.Next(1950, 2005);
            int month = rnd.Next(1, 13);
            int day = rnd.Next(1, 28);
            newSeller.Birthday = new DateTime(year, month, day);
            if (newSeller.Birthday != null)
            {

                int age = DateTime.Now.Year - newSeller.Birthday.Year;
                if (DateTime.Now.Month < newSeller.Birthday.Month || (DateTime.Now.Month < newSeller.Birthday.Month &&
                                                                      DateTime.Now.Day < newSeller.Birthday.Day))
                {
                    age--;
                }

                newSeller.Age = age;
            }
            await _sellersService.Create(newSeller);

            return CreatedAtAction(nameof(Get), new { id = newSeller.Id }, newSeller);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Seller updateSeller)
        {
            var seller = await _sellersService.Get(id);

            if (seller is null)
            {
                return NotFound();
            }

            updateSeller.Id = seller.Id;

            await _sellersService.Update(id, updateSeller);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var seller = await _sellersService.Get(id);

            if (seller is null)
            {
                return NotFound();
            }

            await _sellersService.Remove(id);

            return NoContent();
        }
    }
}
