using System.Collections.Generic;
using Domain.Entities;

namespace DataServices.Interfaces
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        public IEnumerable<BlogPost> GetAllBlogPostsWithLastTwoComments();
    }
}
