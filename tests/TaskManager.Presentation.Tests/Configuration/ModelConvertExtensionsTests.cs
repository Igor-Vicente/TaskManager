using FluentAssertions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Presentation.Configuration;

namespace TaskManager.Presentation.Tests.Configuration
{
    public class ModelConvertExtensionsTests
    {
        [Fact(DisplayName = "Mappear tarefa para ResponseTarefaViewModel")]
        [Trait("Presentation", "Configuration")]
        public void ToResponseTarefaViewModel_DeveRetornarTarefaViewModel_QuandoMapeamentoCorreto()
        {
            // Arrange
            var tarefa = new Tarefa("tt", null)
            {
                Id = 1,
                Status = Status.Concluida,
                CriadaEm = new DateTime(2023, 10, 26, 10, 0, 0),
                ConcluidaEm = new DateTime(2023, 10, 27, 12, 0, 0)
            };

            // Act
            var response = tarefa.ToResponseTarefaViewModel();

            // Assert
            response.Should().NotBeNull();
            response.Id.Should().Be(tarefa.Id);
            response.Titulo.Should().Be(tarefa.Titulo);
            response.Descricao.Should().Be(tarefa.Descricao);
            response.Status.Should().Be(tarefa.Status);
            response.CriadaEm.Should().Be(tarefa.CriadaEm);
            response.ConcluidaEm.Should().Be(tarefa.ConcluidaEm);
        }

        [Fact(DisplayName = "Mappear lista de tarefas para lista de ResponseTarefaViewModel")]
        [Trait("Presentation", "Configuration")]
        public void ToResponseTarefaViewModel_DeveRetornarListViewModel_QuandoMapeamentoCorreto()
        {
            // Arrange
            var tarefas = new List<Tarefa>
        {
            new Tarefa("tt",null)
            {
                Id = 1,
                Status =Status.Pendente,
                CriadaEm = new DateTime(2023, 10, 26, 10, 0, 0),
                ConcluidaEm = null
            },
            new Tarefa("aa", null)
            {
                Id = 2,
                Status = Status.Concluida,
                CriadaEm = new DateTime(2023, 10, 27, 14, 0, 0),
                ConcluidaEm = new DateTime(2023, 10, 28, 16, 0, 0)
            }
        };

            // Act
            var responses = tarefas.ToResponseTarefaViewModel();

            // Assert
            responses.Should().NotBeNull();
            responses.Should().HaveCount(2);

            responses.Should().Contain(r => r.Id == 1 && r.Titulo == "tt");
            responses.Should().Contain(r => r.Id == 2 && r.Titulo == "aa");

            responses.Should().AllSatisfy(response =>
            {
                var tarefa = tarefas.Find(t => t.Id == response.Id);
                response.Should().NotBeNull();
                response.Id.Should().Be(tarefa.Id);
                response.Titulo.Should().Be(tarefa.Titulo);
                response.Descricao.Should().Be(tarefa.Descricao);
                response.Status.Should().Be(tarefa.Status);
                response.CriadaEm.Should().Be(tarefa.CriadaEm);
                response.ConcluidaEm.Should().Be(tarefa.ConcluidaEm);
            });
        }
    }
}
