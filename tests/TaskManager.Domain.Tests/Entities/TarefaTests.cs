using FluentAssertions;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Tests.Entities
{
    public class TarefaTests
    {
        [Fact(DisplayName = "Nova tarefa válida")]
        [Trait("Domain", "Entities")]
        public void IsValid_DeveRetornarTrue_QuandoTarefaValida()
        {
            //arrange
            var tarefa = new Tarefa("Uma tarefa qualquer", "Uma descrição qualquer");

            //act
            var result = tarefa.IsValid();

            //assert
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Nova tarefa inválida")]
        [Trait("Domain", "Entities")]
        public void IsValid_DeveRetornarFalse_QuandoTarefaInvalida()
        {
            //arrange
            var tarefa = new Tarefa("", "");
            tarefa.ConcluidaEm = DateTime.Now.AddDays(-7);

            //act
            var result = tarefa.IsValid();

            //assert
            result.Should().BeFalse();
            tarefa.ValidationResult.Errors.Should().HaveCount(3);
        }

    }
}
