using System;

namespace profile_service.Core.Domain
{

    public class Profile : BaseEntity
    {

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private Profile()
        {
        }

        public Profile(string name) : this()
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.RaiseEvent(new ProfileCreated(this));
        }

        public static Profile CreateProfile(string name)
        {
            return new Profile(name);
        }
    }
}

