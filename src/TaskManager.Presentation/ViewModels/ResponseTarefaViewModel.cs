using TaskManager.Domain.Enums;

namespace TaskManager.Presentation.ViewModels
{
    public class ResponseTarefaViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime? ConcluidaEm { get; set; }
        public Status Status { get; set; }
    }
}
