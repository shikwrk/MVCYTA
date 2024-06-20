namespace YuantaMVC.Models.Requests
{
    public class SaveLogRequest
    {
        public int DocumentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
