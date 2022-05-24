using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        [HttpGet]
        public async Task<List<Customer>> Get() =>
            await _customersService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var client = await _customersService.Get(id);

            if (client is null)
            {
                return NotFound();
            }

            return client;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer newCustomer)
        {
            Random rnd = new Random();
            int year = rnd.Next(1950, 2005);
            int month = rnd.Next(1, 13);
            int day = rnd.Next(1, 28);
            newCustomer.Birthday = new DateTime(year, month, day);
            if (newCustomer.Birthday != null)
            {

                int age = DateTime.Now.Year - newCustomer.Birthday.Year;
                if (DateTime.Now.Month < newCustomer.Birthday.Month || (DateTime.Now.Month < newCustomer.Birthday.Month &&
                                                                      DateTime.Now.Day < newCustomer.Birthday.Day))
                {
                    age--;
                }

                newCustomer.Age = age;
            }
            await _customersService.Create(newCustomer);

            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Customer updateCustomer)
        {
            var customer = await _customersService.Get(id);

            if (customer is null)
            {
                return NotFound();
            }

            updateCustomer.Id = customer.Id;

            await _customersService.Update(id, updateCustomer);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var client = await _customersService.Get(id);

            if (client is null)
            {
                return NotFound();
            }

            await _customersService.Remove(id);

            return NoContent();
        }
    }
}
