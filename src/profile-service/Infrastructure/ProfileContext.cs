using System;
using System.Net.NetworkInformation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using profile_service.Core.Domain;

namespace profile_service.Infrastructure
{
    public class ProfileContext : DbContext
    {
        private readonly IMediator? _mediator;

        public ProfileContext(DbContextOptions<ProfileContext> options, IMediator? mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfileConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutboxMessageConfiguration).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await this.DispatchEvents();

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public async Task DispatchEvents()
        {
            var entityWithRaisedEvents = ChangeTracker.Entries<BaseEntity>()
                                        .Select(e => e.Entity)
                                        .Where(e => e._domainEvents.Any())
                                        .ToList();

            foreach (var entity in entityWithRaisedEvents)
            {
                var raisedEvents = entity._domainEvents.ToList();
                entity.ClearEvents();
                foreach (var domainEvent in raisedEvents)
                {
                    await _mediator.Publish(domainEvent).ConfigureAwait(false);
                }

            }
        }
    }
}

