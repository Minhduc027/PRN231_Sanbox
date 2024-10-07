using System.ComponentModel.DataAnnotations;

namespace DEMO_ASP_.NET_CORE_Web_API.Dtos
{
    public class UpdateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Minimum length is 5 characters")]
        [MaxLength(250, ErrorMessage = "Maximum length is 250 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Minimum length is 5 characters")]
        [MaxLength(2500, ErrorMessage = "Maximum length is 2500 characters")]
        public string Description { get; set; } = string.Empty;
    }
}
