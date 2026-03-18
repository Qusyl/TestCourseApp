using Application;
using Application.Interface;
using Domain;
using Domain.Aggregate;
using Infrastructure.Message;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace Infrastructure.BackgroundService
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context) => _context = context;
        public async Task<Result<ApplicationError>> SaveChangesAsync(CancellationToken cts = default)
        {
            try
            {
                var aggregates = _context.ChangeTracker.Entries<IAggregateRoot>().Where(a => a.State != EntityState.Unchanged).ToList();

                var domainEvents = aggregates.SelectMany(a => a.Entity.Events).ToList();

                foreach (var domEvent in domainEvents)
                {
                    var message = new OutboxMessage
                    {
                        Id = Guid.NewGuid(),
                        Type = domEvent.EventType,
                        Payload = JsonSerializer.Serialize(domEvent),
                        OccurredOn = domEvent.OccurredOn
                    };

                    _context.Messages.Add(message);
                }

                await _context.SaveChangesAsync(cts);

                foreach (var aggregate in aggregates)
                {
                    aggregate.Entity.ClearEvents();
                }
                return Result<ApplicationError>.Success;
            }
            catch (DbUpdateConcurrencyException ex) {
                return Result<ApplicationError>.Failure(ApplicationError.ConcurrencyConflict);
            }
        }
    }
}
