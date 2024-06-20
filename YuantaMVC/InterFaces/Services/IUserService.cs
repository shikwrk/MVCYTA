using YuantaMVC.Models.Dtos;

namespace YuantaMVC.InterFaces.Services
{
    public interface IUserService
    {
        UserDto Authenticate(string username, string password);
        (string hash, string salt) HashPassword(string password);
    }
}
