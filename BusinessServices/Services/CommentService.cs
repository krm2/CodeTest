using System;
using System.Threading.Tasks;
using BusinessServices.Interfaces;
using DataServices.Interfaces;
using Domain.Entities;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace BusinessServices.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CommentService> _logger;

        public CommentService(IUnitOfWork unitOfWork, ILogger<CommentService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task AddComment(CreateCommentDto commentDto, string userName)
        {
            try
            {
                var postComment = new PostComment
                {
                    Id = Guid.NewGuid(),
                    CommentText = commentDto.CommentText,
                    CreatedDt = DateTime.Now,
                    UpdatedDt = DateTime.Now,
                    CreatedBy = userName,
                    BlogPostId = commentDto.BlogPostId
                };

                _unitOfWork.PostComments.Add(postComment);

                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError($"LogError saving new comment: {commentDto.CommentText}", ex);
                throw;
            }
        }

        public async Task DeleteComment(Guid id)
        {
            try
            {
                var postComment = _unitOfWork.PostComments.GetById(id);

                _unitOfWork.PostComments.Remove(postComment);

                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError($"LogError removing new comment: {id}", ex);
                throw;
            }
        }
    }
}
