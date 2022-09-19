using profile_service.Core.Domain;

namespace profile_service.Infrastructure
{
    public interface IProfileRepository
    {
        public Task Save(Profile profile);
    }
}

