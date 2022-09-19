using MediatR;

namespace profile_service.Core.Domain
{
    public abstract class DomainEvent : INotification
    {
        public DateTime CreatedDate { get; set; }
    }
}

