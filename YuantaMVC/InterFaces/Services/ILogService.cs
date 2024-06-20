using YuantaMVC.Models.Dtos;

namespace YuantaMVC.InterFaces.Services
{
    public interface ILogService
    {
        List<SaveLogDto> GetLogs(string userId);
        void SaveLog(SaveLogDto readingLogDto);
    }
}
