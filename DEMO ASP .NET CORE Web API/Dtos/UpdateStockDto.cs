using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEMO_ASP_.NET_CORE_Web_API.Dtos
{
    public class UpdateStockDto
    {
        [Required]
        [MaxLength(25, ErrorMessage = "Maximum length is 25 characters")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MinLength(3, ErrorMessage = "Minimum length is 3 characters")]
        [MaxLength(250, ErrorMessage = "Maximum length is 250 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 10000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum length is 3 characters")]
        [MaxLength(250, ErrorMessage = "Maximum length is 250 characters")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 9000000000)]
        public long MarketCap { get; set; }
    }
}