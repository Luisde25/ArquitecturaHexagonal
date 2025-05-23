using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;

namespace AdminLibrary.Admin.SharedKernel
{
    public abstract class  EntityBase<T> where T : unmanaged
    {
        public T Id { get; set; }
        public string CreateUser { get; set; } = "Admin";
        public DateTime CreateDate { get; set; } = DateTime.UtcNow.AddHours(-5);
        public string? UpdateUser { get; set; } 
        public DateTime? UpdateDate { get; set; }

        private readonly List<DomainEventBase> _domainEventBases = new();

        [NotMapped]
        public IEnumerable<DomainEventBase> DomainEvents => _domainEventBases.AsReadOnly(); 

        protected void RegisterDomainEvent(DomainEventBase domainEvents)
        {
            _domainEventBases.Add(domainEvents);
        }
        internal void ClearDomainEvents()
        {
            _domainEventBases.Clear();  
        }
    }
}
