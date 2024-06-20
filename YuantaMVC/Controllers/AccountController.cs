using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YuantaMVC.InterFaces.Services;
using YuantaMVC.Models.Requests;
using YuantaMVC.Models.ViewModels;

namespace YuantaMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;
        #region public
        public AccountController(IJwtService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登入驗證
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var user = _userService.Authenticate(model.username, model.password);
            if (user != null)
            {
                var token = _jwtService.GenerateToken(user.Id.ToString(), user.Username, user.Role);
                SetResponseCookies("token", token, true, true);
                SetResponseCookies("role", user.Role, false, true);
                SetResponseCookies("username", user.Username, false, true);
                return Ok(new { token });
            }
            return Unauthorized();
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            Response.Cookies.Delete("role");
            Response.Cookies.Delete("username");
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region private
        /// <summary>
        /// 設置Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="isHttpOnly"></param>
        /// <param name="isSecure"></param>
        private void SetResponseCookies(string key, string value, bool isHttpOnly, bool isSecure)
        {
            Response.Cookies.Append(key, value, new CookieOptions
            {
                HttpOnly = isHttpOnly,
                Secure = isSecure,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });
        }
        #endregion
    }
}
