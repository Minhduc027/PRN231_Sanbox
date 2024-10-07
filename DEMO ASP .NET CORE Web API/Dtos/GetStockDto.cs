using DEMO_ASP_.NET_CORE_Web_API.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEMO_ASP_.NET_CORE_Web_API.Dtos
{
    public class GetStockDto
    {
        public string Action {  get; set; }
        public List<Stock> Stock { get; set; }
    }
}