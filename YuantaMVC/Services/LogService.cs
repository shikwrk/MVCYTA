using YuantaMVC.InterFaces.Repositories;
using YuantaMVC.InterFaces.Services;
using YuantaMVC.Models;
using YuantaMVC.Models.Dtos;

namespace YuantaMVC.Services
{
    public class LogService : ILogService
    {
        private readonly IRepository<ReadLog> _logRepository;

        public LogService(IRepository<ReadLog> logRepository)
        {
            _logRepository = logRepository;
        }

        /// <summary>
        /// 根據userid獲得閱讀紀錄
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<SaveLogDto> GetLogs(string userId)
        {
            return _logRepository.GetAll()
                .Where(log => log.UserId.ToString() == userId)
                .Select(log => new SaveLogDto
                {
                    UserId = log.UserId,
                    DocumentId = log.DocumentId,
                    StartTime = log.StartTime,
                    EndTime = log.EndTime,
                    ClientIp = log.ClientIp
                })
                .ToList();
        }

        /// <summary>
        /// 儲存閱讀紀錄
        /// </summary>
        /// <param name="readingLogDto"></param>
        public void SaveLog(SaveLogDto readingLogDto)
        {
            var log = new ReadLog
            {
                UserId = readingLogDto.UserId,
                DocumentId = readingLogDto.DocumentId,
                StartTime = readingLogDto.StartTime,
                EndTime = readingLogDto.EndTime,
                ClientIp = readingLogDto.ClientIp
            };

            _logRepository.Insert(log);
            _logRepository.Save();
        }
    }
}
