namespace TaskManager.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
