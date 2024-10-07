namespace DEMO_ASP_.NET_CORE_Web_API.Helper
{
    public class QueryObject
    {
        public string? CompanyName { get; set; } = null;
        public string? Symbol { get; set; } = null;
        public string? Industry { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool isDescending { get; set; } = false;
    }
}
