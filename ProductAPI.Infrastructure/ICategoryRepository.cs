using ProductAPI.Data.Entities;
using ProductAPI.Data.ViewModel;

namespace ProductAPI.Infrastructure
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Pagination<Category>> GetAllPaging(string? filter, int pageIndex, int pageSize);
        Task<Category> GetById(Guid? id);
        Task<RepositoryResponse> Create(CreateCategoryViewModel model);
        Task<RepositoryResponse> Update(Guid id, UpdateCategoryViewModel model);
        Task<int> Delete(Guid id);
    }
}
