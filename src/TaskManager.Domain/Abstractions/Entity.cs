using FluentValidation.Results;

namespace TaskManager.Domain.Abstractions
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool IsValid();
    }
}
