using FluentAssertions;
using Moq;
using TaskManager.Application.Queries;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Tests.Queries
{
    public class TarefaQueriesTests
    {
        private readonly Mock<ITarefaRepository> _repositoryMock;
        private readonly ITarefaQueries _queries;

        public TarefaQueriesTests()
        {
            _repositoryMock = new Mock<ITarefaRepository>();
            _queries = new TarefaQueries(_repositoryMock.Object);
        }


        [Fact(DisplayName = "Retornar uma tarefa por Id")]
        [Trait("Application", "Queries")]
        public async Task ObterTarefaAsync_DeveRetornarUmaTarefa_QuandoIdEncontrado()
        {
            //arrange
            var tarefa = new Tarefa("f", null);
            _repositoryMock.Setup(s => s.ObterPorIdAsync(tarefa.Id)).ReturnsAsync(tarefa);

            //act
            var result = await _queries.ObterTarefaAsync(tarefa.Id);

            //assert
            result.Should().NotBeNull().And.BeOfType<Tarefa>();
        }

        [Fact(DisplayName = "Retornar lista de tarefas")]
        [Trait("Application", "Queries")]
        public async Task ObterTarefaAsync_DeveRetornarListaDeTarefas_QuandoSizeMaiorQueZero()
        {
            //arrange
            int index = 1;
            int size = 10;
            var tarefas = new List<Tarefa>()
            {
                new Tarefa("a", null),
                new Tarefa("b", null)
            };

            _repositoryMock.Setup(s => s.ObterTodosAsync(index, size, null)).ReturnsAsync(tarefas);

            //act
            var result = await _queries.ObterTarefasAsync(index, size);

            //assert
            result.Should().HaveCount(2);
        }
    }
}
