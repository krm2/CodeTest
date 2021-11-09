using System;
using System.Threading.Tasks;

namespace DataServices.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBlogPostRepository BlogPosts { get; }
        IPostCommentRepository PostComments { get; }
        Task<int> Complete();
    }
}
