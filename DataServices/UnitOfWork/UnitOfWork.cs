using System.Threading.Tasks;
using DataServices.Interfaces;
using DataServices.Repository;

namespace DataServices.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            BlogPosts = new BlogPostRepository(_context);
            PostComments = new PostCommentRepository(_context);
        }

        public IBlogPostRepository BlogPosts { get; }
        public IPostCommentRepository PostComments { get; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
