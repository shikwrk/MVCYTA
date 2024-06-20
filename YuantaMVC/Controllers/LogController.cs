using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YuantaMVC.InterFaces.Services;

namespace YuantaMVC.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 根據userid獲得Log資訊
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetLogs(string userId)
        {
            var logs = _logService.GetLogs(userId);
            return Ok(logs);
        }
    }
}
