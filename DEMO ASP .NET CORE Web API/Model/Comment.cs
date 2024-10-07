namespace DEMO_ASP_.NET_CORE_Web_API.Model
{
    public class Comment
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public long? StockId { get; set; }

    }
}
