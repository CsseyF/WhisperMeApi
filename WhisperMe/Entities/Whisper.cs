namespace WhisperMe.Entities
{
    public class Whisper
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public int UserId { get; set; }
        public string? Message { get; set; }
        public string? Color { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
