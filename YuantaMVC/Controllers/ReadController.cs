using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YuantaMVC.InterFaces.Services;
using YuantaMVC.Models.Dtos;
using YuantaMVC.Models.Requests;
using YuantaMVC.Models.ViewModels;

namespace YuantaMVC.Controllers
{
    public class ReadController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly ILogService _logService;

        public ReadController(IDocumentService documentService, ILogService logService)
        {
            _documentService = documentService;
            _logService = logService;
        }

        public IActionResult Index()
        {
            return View("ReadPage");
        }
        /// <summary>
        /// 根據id獲得對應文件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public IActionResult GetDocument([FromBody]DocumentRequest request)
        {
            if (request == null || request.DocumentId <= 0)
            {
                return BadRequest("Invalid document ID.");
            }
            var document = _documentService.GetDocument(request.DocumentId);
            return PartialView("_DocumentPartial", document);
        }
        /// <summary>
        /// 儲存log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public IActionResult SaveLog([FromBody]SaveLogRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var clientIp = HttpContext.Connection.RemoteIpAddress.ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                _logService.SaveLog(new SaveLogDto
                {
                    UserId = int.Parse(userId),
                    DocumentId = request.DocumentId,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    ClientIp = clientIp
                });

                return Ok();
            }
            return Unauthorized();

        }

    }
}
