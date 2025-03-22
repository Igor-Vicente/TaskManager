using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Queries
{
    public interface ITarefaQueries
    {
        Task<Tarefa?> ObterTarefaAsync(int id);
        Task<IEnumerable<Tarefa>> ObterTarefasAsync(int index, int size, Status? status = null);
    }
}
