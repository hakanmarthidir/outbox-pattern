namespace profile_service.Core.Domain
{
    public class OutboxMessage : BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public string MessageType { get; private set; }
        public string MessageContent { get; private set; }
        public bool IsActive { get; private set; }


        private OutboxMessage()
        {
        }

        public OutboxMessage(string messageType, string content)
        {
            this.MessageType = messageType;
            this.MessageContent = content;
            this.IsActive = true;
            this.CreatedDate = DateTime.Now;
            this.Id = Guid.NewGuid();
        }

        public static OutboxMessage CreateOutboxMessage(string messageType, string content)
        {
            return new OutboxMessage(messageType, content);
        }
    }
}

