using System;
using System.Threading.Tasks;
using Domain.Models;

namespace BusinessServices.Interfaces
{
    public interface ICommentService
    {
        public Task AddComment(CreateCommentDto postComment, string user);

        public Task DeleteComment(Guid postComment);
    }
}
