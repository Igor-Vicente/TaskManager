using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;

namespace TaskManager.Infrastructure.Data.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly ApplicationContext _context;

        public TarefaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AdicionarAsync(Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
        }

        public void Atualizar(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
        }

        public async Task<Tarefa?> ObterPorIdAsync(int id)
        {
            return await _context.Tarefas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Tarefa>> ObterTodosAsync(int index, int size, Status? status = null)
        {
            return await _context.Tarefas
                 .AsNoTracking()
                 .Where(t => status == null || t.Status == status)
                 .Skip((index - 1) * size)
                 .Take(size).ToListAsync();
        }

        public void Remover(Tarefa tarefa)
        {
            _context.Remove(tarefa);
        }
    }
}
