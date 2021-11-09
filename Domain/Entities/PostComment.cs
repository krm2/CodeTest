using System;

namespace Domain.Entities
{
    public class PostComment : BaseEntity
    {
        public Guid Id { get; set; }
        public string CommentText { get; set; }

        public Guid BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
