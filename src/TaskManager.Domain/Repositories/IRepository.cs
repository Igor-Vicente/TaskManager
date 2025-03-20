using TaskManager.Domain.Abstractions;

namespace TaskManager.Domain.Repositories
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
