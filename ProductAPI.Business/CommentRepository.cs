using Microsoft.EntityFrameworkCore;
using ProductAPI.Data.EF;
using ProductAPI.Data.Entities;
using ProductAPI.Data.ViewModel;
using ProductAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Business
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataDbContext _context;

        public CommentRepository(DataDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comments
                            .OrderByDescending(p => p.CreatedDate)
                            .ToListAsync();
        }

        public async Task<Pagination<Comment>> GetAllPaging(Guid? productId, string? filter, int pageIndex, int pageSize)
        {
            var query = from pr in _context.Products
                        join c in _context.Comments on pr.Id equals c.ProductId
                        select new { pr, c };

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.c.Content.Contains(filter)
                || x.c.Content.Contains(filter));
            }

            if (productId.HasValue)
            {
                query = query.Where(x => x.pr.CategoryId == productId.Value);
            }

            var totalRecords = await query.CountAsync();

            var items = await query.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new Comment()
                {
                    Id = u.c.Id,
                    Content = u.c.Content,
                    Author = u.c.Author,
                    Email = u.c.Email,
                    CreatedDate = u.c.CreatedDate,
                    IsAnonymous = u.c.IsAnonymous,
                    ProductId = u.c.ProductId
                })
                .ToListAsync();

            var pagination = new Pagination<Comment>
            {
                Items = items,
                TotalRecords = totalRecords,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            return pagination;
        }


        public async Task<Comment?> GetById(Guid? id)
        {
            var item = await _context.Comments
                            .OrderByDescending(p => p.CreatedDate)
                            .DefaultIfEmpty()
                            .FirstOrDefaultAsync(p => p.Id == id);

            return item;

        }

        public async Task<RepositoryResponse> Create(CreateCommentViewModel model)
        {
            Comment item = new Comment()
            {
                Content = model.Content,
                Author = model.Author,
                Email = model.Email,
                CreatedDate = DateTime.Now,
                IsAnonymous = model.IsAnonymous,
                ProductId = model.ProductId
            };

            _context.Comments.Add(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = item.Id
            };
        }

        public async Task<RepositoryResponse> Update(Guid id, UpdateCommentViewModel model)
        {
            var item = await _context.Comments.FindAsync(id);
            item.Content = model.Content;
            item.Author = model.Author;
            item.Email = model.Email;
            item.CreatedDate = DateTime.Now;
            item.IsAnonymous = model.IsAnonymous;
            item.ProductId = model.ProductId;

            _context.Comments.Update(item);
            var result = await _context.SaveChangesAsync();

            return new RepositoryResponse()
            {
                Result = result,
                Id = id
            };

        }

        public async Task<int> Delete(Guid id)
        {
            var item = await _context.Comments.FindAsync(id);

            _context.Comments.Remove(item);
            var result = await _context.SaveChangesAsync();

            return result;
        }

    }
}
