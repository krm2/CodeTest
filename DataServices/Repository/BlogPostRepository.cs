using System.Collections.Generic;
using System.Linq;
using DataServices.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataServices.Repository
{
    public class BlogPostRepository : Repository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<BlogPost> GetAllBlogPostsWithLastTwoComments()
        {
            return _context.BlogPosts.Include(x => x.PostComments.OrderByDescending(post => post.CreatedDt)
                .Take(2));
        }
    }
}
