using profile_service.Core.Domain;

namespace profile_service.Infrastructure
{
    public class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly ProfileContext _context;
        public OutboxMessageRepository(ProfileContext context)
        {
            _context = context;
        }

        public async Task Save(OutboxMessage message)
        {
            await this._context.OutboxMessages.AddAsync(message);
        }
    }
}


