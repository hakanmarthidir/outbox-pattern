namespace profile_service.Core.Domain
{
    public class ProfileCreated : DomainEvent
    {
        public Profile Profile { get; }
        public ProfileCreated(Profile createdProfile)
        {
            this.Profile = createdProfile;
        }

    }
}

