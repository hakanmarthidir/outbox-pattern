namespace profile_service.Infrastructure
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();
        IOutboxMessageRepository OutboxMessageRepository { get; }
        IProfileRepository ProfileRepository { get; }
    }
}


