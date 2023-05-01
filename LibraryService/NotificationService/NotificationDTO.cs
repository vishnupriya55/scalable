namespace NotificationService
{
    public class NotificationDTO
    {
        public string Message { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
        public bool MarkAsRead { get; set;}
    }
}
