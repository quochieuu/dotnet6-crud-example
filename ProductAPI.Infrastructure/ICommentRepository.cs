using ProductAPI.Data.Entities;
using ProductAPI.Data.ViewModel;

namespace ProductAPI.Infrastructure
{
    public interface ICommentRepository
    {
        Task<Pagination<Comment>> GetAllPaging(Guid? productId, string? filter, int pageIndex, int pageSize);
        Task<Comment> GetById(Guid? id);
        Task<RepositoryResponse> Create(CreateCommentViewModel model);
        Task<RepositoryResponse> Update(Guid id, UpdateCommentViewModel model);
        Task<int> Delete(Guid id);
    }
}
