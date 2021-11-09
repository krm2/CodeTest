using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace BusinessServices.Interfaces
{
    public interface IBlogService
    {
        public Task CreateBlog(CreateBlogPostDto blogPost, string userName);

        public IEnumerable<BlogPostDto> GetPostsWithLatestComments();
    }
}
