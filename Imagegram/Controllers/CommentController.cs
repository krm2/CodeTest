using System;
using System.Threading.Tasks;
using BusinessServices.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Imagegram.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService, ILogger<CommentController> logger)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto commentDto)
        {
            try
            {
                //var userName = User.FindFirstValue(ClaimTypes.Name);

                await _commentService.AddComment(commentDto, "Test");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating comment {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(Guid guid)
        {
            try
            {
                await _commentService.DeleteComment(guid);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting comment {guid} {ex.Message}");
            }
        }
    }
}
