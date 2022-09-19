namespace profile_service.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ProfileContext _profileContext;
        public UnitOfWork(ProfileContext profileContext)
        {
            _profileContext = profileContext;
        }

        private IOutboxMessageRepository _outboxMessageRepository { get; }
        public IOutboxMessageRepository OutboxMessageRepository
        {
            get { return this._outboxMessageRepository ?? new OutboxMessageRepository(_profileContext); }
        }

        private IProfileRepository _profileRepository { get; }
        public IProfileRepository ProfileRepository
        {
            get { return _profileRepository ?? new ProfileRepository(_profileContext); }
        }

        public Task<int> SaveAsync()
        {
            return this._profileContext.SaveChangesAsync(default(CancellationToken));
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._profileContext != null)
                {
                    this._profileContext.Dispose();
                }
            }
        }
    }
}


