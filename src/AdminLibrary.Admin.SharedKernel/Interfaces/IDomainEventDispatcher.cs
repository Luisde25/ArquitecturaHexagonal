namespace AdminLibrary.Admin.SharedKernel.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAndClearEvents(IEnumerable<EntityBase<int>> entityBases);
    }
}
