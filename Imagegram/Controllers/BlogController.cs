using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessServices.Interfaces;
using Domain.Models;

namespace Imagegram.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostDto postDto)
        {
            try
            {
                //var userName = User.FindFirstValue(ClaimTypes.Name);

                await _blogService.CreateBlog(postDto, "Test");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating post {ex.Message}");
            }
        }

        [HttpGet]
        public IEnumerable<BlogPostDto> GetBlogLatest()
        {
            return _blogService.GetPostsWithLatestComments();
        }
    }
}
