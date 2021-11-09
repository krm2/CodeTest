using DataServices.Interfaces;
using Domain.Entities;

namespace DataServices.Repository
{
    public class PostCommentRepository : Repository<PostComment>, IPostCommentRepository
    {
        public PostCommentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
