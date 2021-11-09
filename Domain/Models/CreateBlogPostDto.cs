using Microsoft.AspNetCore.Http;

namespace Domain.Models
{
    public class CreateBlogPostDto
    {
        public string CaptionText { get; set; }
        public IFormFile Image { get; set; }
    }
}
