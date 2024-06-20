namespace YuantaMVC.Models.Dtos
{
    public class SaveLogDto
    {
        public int UserId { get; set; }
        public int DocumentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ClientIp { get; set; }
    }
}
