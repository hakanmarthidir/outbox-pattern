using System;
using MediatR;
using profile_service.Core.Domain;
using profile_service.Infrastructure;

namespace profile_service.Core.Application.Profile.Handlers
{
    public class ProfileCreatedEventHandler : INotificationHandler<ProfileCreated>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProfileCreatedEventHandler(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task Handle(ProfileCreated notification, CancellationToken cancellationToken)
        {
            var eventData = System.Text.Json.JsonSerializer.Serialize(notification.Profile);
            OutboxMessage message = OutboxMessage.CreateOutboxMessage(typeof(ProfileCreated).Name, eventData);

            await this._unitOfWork.OutboxMessageRepository.Save(message);

        }
    }
}

