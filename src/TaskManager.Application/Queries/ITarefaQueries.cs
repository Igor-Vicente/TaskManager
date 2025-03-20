using TaskManager.Domain.Entities;

namespace TaskManager.Application.Queries
{
    public interface ITarefaQueries
    {
        Task<Tarefa?> ObterTarefaAsync(int id);
        Task<IEnumerable<Tarefa>> ObterTarefasAsync(int index = 1, int size = 10);
    }
}
