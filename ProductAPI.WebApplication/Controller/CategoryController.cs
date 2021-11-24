using Microsoft.AspNetCore.Mvc;
using ProductAPI.Common;
using ProductAPI.Data.ViewModel;
using ProductAPI.Infrastructure;

namespace ProductAPI.WebApplication.Controller
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repo;

        public CategoryController(ICategoryRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _repo.GetAll());
        }

        [HttpGet("filter")]
        public async Task<ActionResult> GetAllPaging(string? filter, int pageIndex, int pageSize)
        {
            return Ok(await _repo.GetAllPaging(filter, pageIndex, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {

            var item = await _repo.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Category with id: {id} is not found"));
            }

            return Ok(item);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post(CreateCategoryViewModel model)
        {
            var result = await _repo.Create(model);

            if(result.Result > 0)
            {
                return RedirectToAction(nameof(Get), new { id = result.Id });
            } else
            {
                return BadRequest(new ApiBadRequestResponse("Create category failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(UpdateCategoryViewModel model, Guid id)
        {
            var item = await _repo.GetById(id);
            if(item == null)
                return NotFound(new ApiNotFoundResponse($"Category with id: {id} is not found"));

            if (id == model.ParentId)
            {
                return BadRequest(new ApiBadRequestResponse("Category cannot be a child itself."));
            }


            var result = await _repo.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update category failed"));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete (Guid id)
        {
            var item = _repo.GetById(id);

            if(item == null)
                return NotFound(new ApiNotFoundResponse($"Category with id: {id} is not found"));

            var result = await _repo.Delete(id);

            if (result > 0)
            {
                return Ok();
            } else
            {
                return BadRequest(new ApiBadRequestResponse("Delete category failed"));
            }
                
        }

    }
}
