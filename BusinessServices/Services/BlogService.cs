using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessServices.Interfaces;
using DataServices.Interfaces;
using Domain.Entities;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace BusinessServices.Services
{
    public class BlogService : IBlogService
    {
        private readonly IImageService _imageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BlogService> _logger;

        public BlogService(IImageService imageService, IUnitOfWork unitOfWork, ILogger<BlogService> logger)
        {
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task CreateBlog(CreateBlogPostDto blogPostDto, string userName)
        {
            var fileName = blogPostDto.Image.FileName;

            var fileNameNoEx = Path.GetFileNameWithoutExtension(fileName);

            var extension = Path.GetExtension(fileName);

            var imageName = $"{fileNameNoEx}{DateTime.Now:yymmssfff}{extension}";

            var blogPost = new BlogPost
            {
                Id = Guid.NewGuid(),
                CreatedDt = DateTime.Now,
                UpdatedDt = DateTime.Now,
                CreatedBy = userName,
                CaptionText = blogPostDto.CaptionText,
                ImageName = imageName,
                PostComments = new List<PostComment>()
            };

            _logger.LogInformation($"Saving new Blog {blogPost.Id}");

            
            try
            {
                _unitOfWork.BlogPosts.Add(blogPost);

                await _unitOfWork.Complete();

                _logger.LogInformation("Saved new Blog Post");

                _logger.LogInformation($"Saving Original Image: {blogPost.ImageName}");

                await SaveImages(blogPostDto, blogPost);

            }
            catch(Exception ex)
            {
                _logger.LogError($"LogError creating blog post {ex.Message}", ex);

                throw;
            }
        }

        public IEnumerable<BlogPostDto> GetPostsWithLatestComments()
        {
            _logger.LogInformation("Getting posts with latest comments");

            var blogPosts = _unitOfWork.BlogPosts.GetAllBlogPostsWithLastTwoComments();

            //I'm sorry! :D
            var blogPostDtos = blogPosts.Select(x => new BlogPostDto
            {
                CaptionText = x.CaptionText,
                Image = _imageService.LoadJpeg(x.ImageName),
                Comments = x.PostComments.Select(c => new CommentDto
                    {CommentText = c.CommentText, CreatedAt = c.CreatedDt, LastUpdatedAt = c.UpdatedDt, Id = c.Id}),
                Owner = x.CreatedBy,
                Id = x.Id,
                CreatedAt = x.CreatedDt,
                LastUpdatedAt = x.UpdatedDt
            });

            return blogPostDtos;
        }

        private async Task SaveImages(CreateBlogPostDto blogPostDto, BlogPost blogPost)
        {
            await using (var stream = blogPostDto.Image.OpenReadStream())
            {
                await using (var ms = new MemoryStream())
                {
                    await stream.CopyToAsync(ms);

                    var imageBytes = ms.ToArray();

                    await _imageService.SaveOriginalImage(imageBytes, blogPost.ImageName);

                    _logger.LogInformation($"Saved Original Image: {blogPost.ImageName}");

                    _logger.LogInformation($"Converting Image to JPEG: {blogPost.ImageName}");

                    _imageService.SaveAsJpeg(imageBytes, blogPost.ImageName);

                    _logger.LogInformation($"Saved Image {blogPost.ImageName} as JPEG");
                }
            }
        }
    }
}
