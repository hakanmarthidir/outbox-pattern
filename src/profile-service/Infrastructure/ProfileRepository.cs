using Microsoft.EntityFrameworkCore;
using profile_service.Core.Domain;

namespace profile_service.Infrastructure
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ProfileContext _context;

        public ProfileRepository(ProfileContext context)
        {
            _context = context;
        }
        public async Task Save(Profile profile)
        {
            await this._context.Profiles.AddAsync(profile);
        }
    }
}

