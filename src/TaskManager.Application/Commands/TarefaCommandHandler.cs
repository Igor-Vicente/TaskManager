using MediatR;
using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Commands
{
    public sealed class TarefaCommandHandler : IRequestHandler<AdicionarTarefaCommand, Tarefa>,
                                        IRequestHandler<AtualizarTarefaCommand, Tarefa>,
                                        IRequestHandler<RemoverTarefaCommand, Tarefa>
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly INotificador _notificador;

        public TarefaCommandHandler(ITarefaRepository tarefaRepository, INotificador notificador)
        {
            _tarefaRepository = tarefaRepository;
            _notificador = notificador;
        }

        public async Task<Tarefa> Handle(AdicionarTarefaCommand request, CancellationToken cancellationToken)
        {
            var tarefa = new Tarefa(request.Titulo, request.Descricao);

            if (!tarefa.IsValid())
            {
                _notificador.AdicionarNotificacao(tarefa.ValidationResult.Errors.Select(e => e.ErrorMessage));
                return null;
            }

            await _tarefaRepository.AdicionarAsync(tarefa);
            await _tarefaRepository.UnitOfWork.CommitAsync();
            return tarefa;
        }

        public async Task<Tarefa> Handle(AtualizarTarefaCommand request, CancellationToken cancellationToken)
        {
            var tarefa = await _tarefaRepository.ObterPorIdAsync(request.TarefaId);

            if (tarefa == null)
            {
                _notificador.AdicionarNotificacao("Tarefa não encontrada");
                return null;
            }

            if (tarefa.Status == Status.Concluida)
            {
                _notificador.AdicionarNotificacao("Tarefa já foi concluída");
                return null;
            }

            tarefa.Titulo = request.Titulo;
            tarefa.Descricao = request.Descricao;
            tarefa.Status = request.Status;
            tarefa.ConcluidaEm = request.ConcluidaEm;

            if (!tarefa.IsValid())
            {
                _notificador.AdicionarNotificacao(tarefa.ValidationResult.Errors.Select(e => e.ErrorMessage));
                return null;
            }

            _tarefaRepository.Atualizar(tarefa);
            await _tarefaRepository.UnitOfWork.CommitAsync();
            return tarefa;
        }

        public async Task<Tarefa> Handle(RemoverTarefaCommand request, CancellationToken cancellationToken)
        {
            var tarefa = await _tarefaRepository.ObterPorIdAsync(request.TarefaId);

            if (tarefa == null)
            {
                _notificador.AdicionarNotificacao("Tarefa não encontrada");
                return null;
            }

            _tarefaRepository.Remover(tarefa);
            await _tarefaRepository.UnitOfWork.CommitAsync();
            return tarefa;
        }
    }
}
