using AuthApi.Models;

namespace AuthApi.Service.Iservice
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
