using ProductAPI.Data.Entities;
using ProductAPI.Data.ViewModel;

namespace ProductAPI.Infrastructure
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Pagination<ProductQuickViewModel>> GetAllPaging(string? filter, Guid? categoryId, int pageIndex, int pageSize);
        Task<Product> GetById(Guid? id);
        Task<RepositoryResponse> Create(CreateProductViewModel model);
        Task<RepositoryResponse> Update(Guid id, UpdateProductViewModel model);
        Task<int> Delete(Guid id);
        Task<int> UpdateViewCount(Guid id);
    }
}
