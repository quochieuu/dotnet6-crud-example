using Microsoft.EntityFrameworkCore;
using ProductAPI.Data.EF;
using ProductAPI.Data.Entities;
using ProductAPI.Data.ViewModel;
using ProductAPI.Infrastructure;

namespace ProductAPI.Business
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataDbContext _context;

        public CategoryRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories
                            .OrderByDescending(p => p.CreatedDate)
                            .ToListAsync();
        }

        public async Task<Pagination<Category>> GetAllPaging(string? filter, int pageIndex, int pageSize)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.Contains(filter)
                || x.Name.Contains(filter));
            }
            var totalRecords = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagination = new Pagination<Category>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            return pagination;
        }

        public async Task<Category?> GetById(Guid? id)
        {
            var item = await _context.Categories
                            .OrderByDescending(p => p.CreatedDate)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;

        }

        public async Task<RepositoryResponse> Create(CreateCategoryViewModel model)
        {
            Category item = new Category()
            {
                Name = model.Name,
                ParentId = model.ParentId,
                CreatedDate = DateTime.Now,
                Status = model.Status,
            };

            _context.Categories.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(Guid id, UpdateCategoryViewModel model)
        {
            var item = await _context.Categories.FindAsync(id);
            item.Name = model.Name;
            item.ParentId = model.ParentId;
            item.CreatedDate = DateTime.Now;
            item.Status = model.Status;

            _context.Categories.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };

        }

        public async Task<int> Delete(Guid id)
        {
            var item = await _context.Categories.FindAsync(id);

            _context.Categories.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }


    }
}
