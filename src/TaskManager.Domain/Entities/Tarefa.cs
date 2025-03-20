using FluentValidation;
using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities
{
    public class Tarefa : Entity
    {
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime CriadaEm { get; set; }
        public DateTime? ConcluidaEm { get; set; }
        public Status Status { get; set; }

        public Tarefa(string titulo, string? descricao)
        {
            Titulo = titulo;
            Descricao = descricao;
            CriadaEm = DateTime.Now;
            Status = Status.Pendente;
        }

        public override bool IsValid()
        {
            ValidationResult = new TarefaValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class TarefaValidator : AbstractValidator<Tarefa>
    {
        public TarefaValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty()
                .WithMessage("Forneça um título")
                .MaximumLength(100)
                .WithMessage("Título deve ter no máximo 100 caracteres");

            RuleFor(x => x.ConcluidaEm)
                .GreaterThan(x => x.CriadaEm)
                .WithMessage("Data de conclusão deve ser posterior à data de criação");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("O status informado é inválido");

            RuleFor(x => x.ConcluidaEm)
                .NotNull()
                .When(x => x.Status == Status.Concluida)
                .WithMessage("A data de conclusão é obrigatória quando o status for 'Concluida'");

            RuleFor(x => x.ConcluidaEm)
                .Null()
                .When(x => x.Status != Status.Concluida)
                .WithMessage("Data de conclusão não deve ser adicionada quando status for diferente de 'Concluida'");
        }
    }
}
