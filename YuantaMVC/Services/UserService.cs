using YuantaMVC.Models.Dtos;
using YuantaMVC.Models;
using YuantaMVC.InterFaces.Repositories;
using YuantaMVC.InterFaces.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace YuantaMVC.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 驗證帳號密碼
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserDto Authenticate(string username, string password)
        {
            var users = _userRepository.GetAll();
            var a = users.FirstOrDefault(u => u.Username == username);
            var user = _userRepository.GetAll().Where(u => u.Username == username).FirstOrDefault();
            if (user == null) 
                return null;

            if (VerifyPassword(password, user.Password, user.Passwordsalt))
            {
                return new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    Role = user.Role
                };
            }
            return null;
        }

        /// <summary>
        /// 生成Hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public (string hash, string salt) HashPassword(string password)
        {
            byte[] saltBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            var salt = Convert.ToBase64String(saltBytes);

            // 用salt與密碼生成Hash
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return (hash, salt);
        }

        /// <summary>
        /// 驗證密碼
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        private bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);

            // 
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hash == storedHash;
        }
    }
}
