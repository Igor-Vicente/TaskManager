namespace TaskManager.Application.Commands
{
    public class AdicionarTarefaCommand : Command
    {
        public string Titulo { get; private set; }
        public string? Descricao { get; private set; }

        public AdicionarTarefaCommand(string titulo, string? descricao)
        {
            Titulo = titulo;
            Descricao = descricao;
        }
    }
}
