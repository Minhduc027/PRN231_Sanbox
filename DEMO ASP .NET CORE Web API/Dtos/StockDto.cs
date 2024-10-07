using System.ComponentModel.DataAnnotations.Schema;

namespace DEMO_ASP_.NET_CORE_Web_API.Dtos
{
    public class StockDto
    {
        public long Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }       
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
