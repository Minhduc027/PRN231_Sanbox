﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DEMO_ASP_.NET_CORE_Web_API.Model
{
    public class Stock
    {
        public long Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase {  get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap {  get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
