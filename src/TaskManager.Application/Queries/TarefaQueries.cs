using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Queries
{
    public class TarefaQueries : ITarefaQueries
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaQueries(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<Tarefa?> ObterTarefaAsync(int id)
        {
            return await _tarefaRepository.ObterPorIdAsync(id);
        }

        public async Task<IEnumerable<Tarefa>> ObterTarefasAsync(int index = 1, int size = 10)
        {
            return await _tarefaRepository.ObterTodosAsync(index, size);
        }
    }
}
