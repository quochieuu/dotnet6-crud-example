using Microsoft.AspNetCore.Mvc;
using ProductAPI.Business.Services.EmailService;
using ProductAPI.Common;
using ProductAPI.Data.ViewModel;
using ProductAPI.Infrastructure;

namespace ProductAPI.WebApplication.Controller
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repo;
        private readonly IEmailSender _emailSender;

        public CommentController(ICommentRepository repo, IEmailSender emailSender)
        {
            _repo = repo;
            _emailSender = emailSender;
        }


        [HttpGet("filter")]
        public async Task<ActionResult> GetAllPaging(Guid? productId, string? filter, int pageIndex, int pageSize)
        {
            return Ok(await _repo.GetAllPaging(productId, filter, pageIndex, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {

            var item = await _repo.GetById(id);

            if (item == null)
            {
                return NotFound(new ApiNotFoundResponse($"Comment with id: {id} is not found"));
            }

            return Ok(item);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post(CreateCommentViewModel model)
        {
            var result = await _repo.Create(model);

            if (result.Result > 0)
            {
                var message = new Message(
                    new string[] { model.Email }, 
                    "Bình luận mới", 
                    $"Bạn vừa thêm bình luận mới. {model.Content}", 
                    null);
                await _emailSender.SendEmailAsync(message);

                return RedirectToAction(nameof(Get), new { id = result.Id });
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Create comment failed"));
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(UpdateCommentViewModel model, Guid id)
        {
            var item = await _repo.GetById(id);
            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Comment with id: {id} is not found"));


            var result = await _repo.Update(id, model);

            if (result.Result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Update comment failed"));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = _repo.GetById(id);

            if (item == null)
                return NotFound(new ApiNotFoundResponse($"Comment with id: {id} is not found"));

            var result = await _repo.Delete(id);

            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiBadRequestResponse("Delete comment failed"));
            }

        }



    }
}
