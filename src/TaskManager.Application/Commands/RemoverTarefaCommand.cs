namespace TaskManager.Application.Commands
{
    public class RemoverTarefaCommand : Command
    {
        public int TarefaId { get; private set; }

        public RemoverTarefaCommand(int tarefaId)
        {
            TarefaId = tarefaId;
        }
    }
}
