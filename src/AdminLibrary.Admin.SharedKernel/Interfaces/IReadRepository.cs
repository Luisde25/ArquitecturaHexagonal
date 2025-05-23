using Ardalis.Specification;
namespace AdminLibrary.Admin.SharedKernel.Interfaces;

    public interface IReadRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {

    }

