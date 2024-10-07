using Microsoft.AspNetCore.Identity;

namespace DEMO_ASP_.NET_CORE_Web_API.Model
{
    public class AppUser : IdentityUser
    {
        public string? Address {  get; set; }
    }
}
