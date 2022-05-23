using VattaAppApi.Models;

namespace VattaAppApi.Services.Interfaces;

public interface ICategoriesService
{
    public Task<List<Category>> Get();

    public Task<Category?> Get(string id);

    public Task Create(Category newCategory);

    public Task Update(string id, Category updateCategory);

    public Task Remove(string id);
}