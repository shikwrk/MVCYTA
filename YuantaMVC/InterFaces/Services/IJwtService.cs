using System.Security.Claims;

namespace YuantaMVC.InterFaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string username, string role);
    }
}
