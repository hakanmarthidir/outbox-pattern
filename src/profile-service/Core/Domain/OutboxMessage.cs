namespace profile_service.Core.Domain
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string MessageType { get; set; }
        public string MessageContent { get; set; }
        public bool IsActive { get; set; }

    }
}

