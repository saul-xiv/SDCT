namespace SDCT.Models
{
    public class StoryDTO
    {
        public string? title { get; set; }
        public string? uri { get; set; }
        public string? postedBy { get; set; }
        public DateTime? time { get; set; }
        public long? score { get; set; }
        public long? commentCount { get; set; }
    }
}
