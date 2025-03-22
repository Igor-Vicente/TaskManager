using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Repositories
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        Task<Tarefa?> ObterPorIdAsync(int id);
        Task<IEnumerable<Tarefa>> ObterTodosAsync(int index, int size, Status? status = null);
        Task AdicionarAsync(Tarefa tarefa);
        void Atualizar(Tarefa tarefa);
        void Remover(Tarefa tarefa);
    }
}
