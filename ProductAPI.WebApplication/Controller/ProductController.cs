using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Common;
using ProductAPI.Data.ViewModel;
using ProductAPI.Infrastructure;

namespace ProductAPI.WebApplication.Controller
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }

        [HttpGet("filter")]
        public async Task<ActionResult> GetAllPaging(string filter, Guid? categoryId, int pageIndex, int pageSize)
        {
            return Ok(await _repo.GetAllPaging(filter, categoryId, pageIndex, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {

            var item = await _repo.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Product with id: {id} is not found"));
            }

            return Ok(item);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post(CreateProductViewModel model)
        {
            var result = await _repo.Create(model);

            if (result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create product failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(UpdateProductViewModel model, Guid id)
        {
            var item = await _repo.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Product with id: {id} is not found"));


            var result = await _repo.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update product failed"));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = _repo.GetById(id);

            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Product with id: {id} is not found"));

            var result = await _repo.Delete(id);

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Delete product failed"));
            }

        }

        [HttpPut("{id}/view-count")]
        public async Task<IActionResult> UpdateViewCount(Guid id)
        {
            var item = _repo.GetById(id);

            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Product with id: {id} is not found"));

            var result = await _repo.UpdateViewCount(id);

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Increase view count failed"));
            }
        }


    }
}
