using Ardalis.Specification;

namespace AdminLibrary.Admin.SharedKernel.Interfaces;

    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
