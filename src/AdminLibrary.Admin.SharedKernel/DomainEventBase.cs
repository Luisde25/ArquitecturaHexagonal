using MediatR;

namespace AdminLibrary.Admin.SharedKernel;

    public abstract class DomainEventBase : INotification
    {
        public DateTime DateOcurres { get; protected set; } = DateTime.UtcNow.AddHours(-5);
    }

