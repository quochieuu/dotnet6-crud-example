using Microsoft.EntityFrameworkCore;
using ProductAPI.Data.EF;
using ProductAPI.Data.Entities;
using ProductAPI.Data.ViewModel;
using ProductAPI.Infrastructure;

namespace ProductAPI.Business
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataDbContext _context;

        public ProductRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                            .OrderByDescending(p => p.CreatedDate)
                            .ToListAsync();
        }

        public async Task<Pagination<ProductQuickViewModel>> GetAllPaging(string filter, Guid? categoryId, int pageIndex, int pageSize)
        {
            var query = from pr in _context.Products
                        join c in _context.Categories on pr.CategoryId equals c.Id
                        select new { pr, c };

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.pr.Name.Contains(filter)
                || x.pr.Name.Contains(filter));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.pr.CategoryId == categoryId.Value);
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new ProductQuickViewModel()
                {
                    Id = u.pr.Id,
                    Name = u.pr.Name,
                    CategoryId = u.pr.CategoryId,
                    Content = u.pr.Content,
                    Price = u.pr.Price,
                    UrlImage = u.pr.UrlImage,
                    ViewCount = u.pr.ViewCount,
                    CreatedDate = u.pr.CreatedDate,
                    Status = u.pr.Status,

                })
                .ToListAsync();

            var pagination = new Pagination<ProductQuickViewModel>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            return pagination;
        }

        public async Task<Product?> GetById(Guid? id)
        {
            var item = await _context.Products
                            .OrderByDescending(p => p.CreatedDate)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;

        }

        public async Task<RepositoryResponse> Create(CreateProductViewModel model)
        {
            Product item = new Product()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Content = model.Content,
                Price = model.Price,
                UrlImage = model.UrlImage,
                CreatedDate = DateTime.Now,
                Status = model.Status,
            };

            _context.Products.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(Guid id, UpdateProductViewModel model)
        {
            var item = await _context.Products.FindAsync(id);
            item.Name = model.Name;
            item.CategoryId = model.CategoryId;
            item.Content = model.Content;
            item.Price = model.Price;
            item.UrlImage = model.UrlImage;
            item.CreatedDate = DateTime.Now;
            item.Status = model.Status;

            _context.Products.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };

        }


        public async Task<int> UpdateViewCount(Guid id)
        {
            var item = await _context.Products.FindAsync(id);

            if (item.ViewCount == null)
                item.ViewCount = 0;

            item.ViewCount += 1;
            _context.Products.Update(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> Delete(Guid id)
        {
            var item = await _context.Products.FindAsync(id);

            _context.Products.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }


    }
}
