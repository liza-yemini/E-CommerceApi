using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;

namespace VattaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly MongoCategoriesService _mongoCategoriesService;

        public CategoriesController(MongoCategoriesService mongoCategoriesService)
        {
            _mongoCategoriesService = mongoCategoriesService;
        }

        [HttpGet]
        public async Task<List<Category>> Get() =>
            await _mongoCategoriesService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Category>> Get(string id)
        {
            var category = await _mongoCategoriesService.Get(id);

            if (category is null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category newCategory)
        {
            await _mongoCategoriesService.Create(newCategory);

            return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Category updateCategory)
        {
            var category = await _mongoCategoriesService.Get(id);

            if (category is null)
            {
                return NotFound();
            }

            updateCategory.Id = category.Id;

            await _mongoCategoriesService.Update(id, updateCategory);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var category = await _mongoCategoriesService.Get(id);

            if (category is null)
            {
                return NotFound();
            }

            await _mongoCategoriesService.Remove(id);

            return NoContent();
        }
    }

}
