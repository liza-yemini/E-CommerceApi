using Microsoft.AspNetCore.Mvc;
using VattaAppApi.Models;
using VattaAppApi.Services;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<List<Category>> Get() =>
            await _categoriesService.Get();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Category>> Get(string id)
        {
            var category = await _categoriesService.Get(id);

            if (category is null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category newCategory)
        {
            await _categoriesService.Create(newCategory);

            return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Category updateCategory)
        {
            var category = await _categoriesService.Get(id);

            if (category is null)
            {
                return NotFound();
            }

            updateCategory.Id = category.Id;

            await _categoriesService.Update(id, updateCategory);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var category = await _categoriesService.Get(id);

            if (category is null)
            {
                return NotFound();
            }

            await _categoriesService.Remove(id);

            return NoContent();
        }
    }

}
