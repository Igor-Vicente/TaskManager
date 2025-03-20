using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Enums;

namespace TaskManager.Presentation.ViewModels
{
    public class AtualizarTarefaViewModel
    {
        [Required(ErrorMessage = "Forneça um título")]
        [MaxLength(100, ErrorMessage = "Título deve ter no máximo 100 caracteres")]
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? ConcluidaEm { get; set; }

        [EnumDataType(typeof(Status), ErrorMessage = "O status informado é inválido")]
        public Status Status { get; set; }
    }
}
