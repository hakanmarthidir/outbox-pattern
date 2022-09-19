using profile_service.Core.Domain;

namespace profile_service.Infrastructure
{
    public interface IOutboxMessageRepository
    {
        public Task Save(OutboxMessage message);
    }
}

