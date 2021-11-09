using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServices;
using DataServices.UnitOfWork;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.RepositoryTests
{
    [TestClass]
    public class RepoTests
    {
        private readonly ApplicationContext _dbContext;

        public RepoTests()
        {
            _dbContext = new InMemoryMockDB().GetInMemAppContext();
        }

        [TestMethod]
        [TestCategory("Repository Tests")]
        public async Task CanAddAndRetrieveBlogPosts()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            var blogPosts = new List<BlogPost>
                {
                    new BlogPost { Id = guid1, CaptionText = "Test 123", CreatedDt = DateTime.Now, UpdatedDt = DateTime.Now, CreatedBy = "Test", ImageName = "Test.Png", PostComments = new List<PostComment>()},
                    new BlogPost { Id = guid2, CaptionText = "Test 124", CreatedDt = DateTime.Now, UpdatedDt = DateTime.Now, CreatedBy = "Test1", ImageName = "Test1.Png", PostComments = new List<PostComment>()}
                };


            var unitOfWork = new UnitOfWork(_dbContext);

            unitOfWork.BlogPosts.AddRange(blogPosts);

            await unitOfWork.Complete();

            var results = unitOfWork.BlogPosts.GetAllBlogPostsWithLastTwoComments().ToList();

            Assert.IsTrue(results.Count == 2);

            Assert.IsNotNull(results.Select(x => x.Id == guid1));
        }

        [TestMethod]
        [TestCategory("Repository Tests")]
        public async Task CanAddPostComment()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            var blogPosts = new List<BlogPost>
            {
                new BlogPost { Id = guid1, CaptionText = "Test 123", CreatedDt = DateTime.Now, UpdatedDt = DateTime.Now, CreatedBy = "Test", ImageName = "Test.Png", PostComments = new List<PostComment>()},
                new BlogPost { Id = guid2, CaptionText = "Test 124", CreatedDt = DateTime.Now, UpdatedDt = DateTime.Now, CreatedBy = "Test1", ImageName = "Test1.Png", PostComments = new List<PostComment>()}
            };

            var unitOfWork = new UnitOfWork(_dbContext);

            unitOfWork.BlogPosts.AddRange(blogPosts);

            await unitOfWork.Complete();

            var commentGuid = Guid.NewGuid();

            var comment = new PostComment
            {
                Id = commentGuid,
                CommentText = "Test",
                BlogPostId = guid1,
                CreatedDt = DateTime.Now,
                UpdatedDt = DateTime.Now,
                CreatedBy = "Test",
            };

            unitOfWork.PostComments.Add(comment);

            await unitOfWork.Complete();

            var result = unitOfWork.PostComments.GetById(commentGuid);

            Assert.AreEqual(comment, result);
        }

        [TestMethod]
        [TestCategory("Repository Tests")]
        public async Task CanDeletePostComment()
        {
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();

            var blogPosts = new List<BlogPost>
            {
                new BlogPost { Id = guid1, CaptionText = "Test 123", CreatedDt = DateTime.Now, UpdatedDt = DateTime.Now, CreatedBy = "Test", ImageName = "Test.Png", PostComments = new List<PostComment>()},
                new BlogPost { Id = guid2, CaptionText = "Test 124", CreatedDt = DateTime.Now, UpdatedDt = DateTime.Now, CreatedBy = "Test1", ImageName = "Test1.Png", PostComments = new List<PostComment>()}
            };

            var unitOfWork = new UnitOfWork(_dbContext);

            unitOfWork.BlogPosts.AddRange(blogPosts);

            await unitOfWork.Complete();

            var commentGuid = Guid.NewGuid();

            var comment = new PostComment
            {
                Id = commentGuid,
                CommentText = "Test",
                BlogPostId = guid1,
                CreatedDt = DateTime.Now,
                UpdatedDt = DateTime.Now,
                CreatedBy = "Test",
            };

            unitOfWork.PostComments.Add(comment);

            await unitOfWork.Complete();

            unitOfWork.PostComments.Remove(comment);

            await unitOfWork.Complete();

            var result = unitOfWork.PostComments.GetById(commentGuid);

            Assert.IsNull(result);
        }
    }
}
