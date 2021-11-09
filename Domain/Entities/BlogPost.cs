using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class BlogPost : BaseEntity
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public string CaptionText { get; set; }

        public ICollection<PostComment> PostComments { get; set; }
    }
}
