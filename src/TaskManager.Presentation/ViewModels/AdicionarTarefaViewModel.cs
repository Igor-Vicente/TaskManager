using System.ComponentModel.DataAnnotations;

namespace TaskManager.Presentation.ViewModels
{
    public struct AdicionarTarefaViewModel
    {
        [Required(ErrorMessage = "Forneça um título")]
        [MaxLength(100, ErrorMessage = "Título deve ter no máximo 100 caracteres")]
        public string Titulo { get; set; }

        public string? Descricao { get; set; }
    }
}
