using AdminLibrary.Admin.SharedKernel.Interfaces;
using MediatR;

namespace AdminLibrary.Admin.SharedKernel;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;   
    }
    public async Task DispatchAndClearEvents(IEnumerable<EntityBase<int>> entityBases)
    {
       foreach (var entity in entityBases)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach(var domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }
    }
}

