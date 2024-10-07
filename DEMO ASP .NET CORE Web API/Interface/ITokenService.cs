using DEMO_ASP_.NET_CORE_Web_API.Model;

namespace DEMO_ASP_.NET_CORE_Web_API.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
