using TaskManager.Domain.Enums;

namespace TaskManager.Application.Commands
{
    public class AtualizarTarefaCommand : Command
    {
        public int TarefaId { get; private set; }
        public string Titulo { get; private set; }
        public Status Status { get; private set; }
        public string? Descricao { get; private set; }
        public DateTime? ConcluidaEm { get; private set; }

        public AtualizarTarefaCommand(int id, string titulo, Status status, string? descricao, DateTime? concluidaEm)
        {
            TarefaId = id;
            Titulo = titulo;
            Status = status;
            Descricao = descricao;
            ConcluidaEm = concluidaEm;
        }
    }
}
