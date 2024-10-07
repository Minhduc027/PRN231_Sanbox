using System.ComponentModel.DataAnnotations;

namespace DEMO_ASP_.NET_CORE_Web_API.Dtos
{
    public class LoginDto
    {
        [Required]
        public string? UserName { get; set; }

        [Required] 
        public string? Password { get; set; }
    }
}
