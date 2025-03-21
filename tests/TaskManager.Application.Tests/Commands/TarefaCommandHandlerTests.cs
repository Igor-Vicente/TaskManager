using FluentAssertions;
using Moq;
using TaskManager.Application.Commands;
using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;

namespace TaskManager.Application.Tests.Commands
{
    public class TarefaCommandHandlerTests
    {
        private readonly TarefaCommandHandler _handler;
        private readonly Mock<ITarefaRepository> _repositoryMock;
        private readonly INotificador _notificador;
        public TarefaCommandHandlerTests()
        {
            _repositoryMock = new Mock<ITarefaRepository>();
            _notificador = new Notificador();
            _handler = new TarefaCommandHandler(_repositoryMock.Object, _notificador);
        }

        [Fact(DisplayName = "Adicionar tarefa válida")]
        [Trait("Application", "Commands")]
        public async Task Handle_DeveRetornarTarefa_QuandoAdicionarTarefaCommandValido()
        {
            //arrange
            var command = new AdicionarTarefaCommand("Nova Tarefa", null);

            _repositoryMock.Setup(c => c.AdicionarAsync(It.IsAny<Tarefa>())).Returns(Task.CompletedTask);
            _repositoryMock.Setup(c => c.UnitOfWork.CommitAsync()).ReturnsAsync(true);

            //act 
            var result = await _handler.Handle(command, default);

            //assert
            result.Should().BeOfType<Tarefa>();
            result.Titulo.Should().Be("Nova Tarefa");
            _repositoryMock.Verify(c => c.AdicionarAsync(It.IsAny<Tarefa>()), Times.Once);
        }

        [Fact(DisplayName = "Adicionar tarefa sem titulo")]
        [Trait("Application", "Commands")]
        public async Task Handle_DeveRetornarNull_QuandoAdicionarTarefaCommandInvalida()
        {
            //arrange
            var command = new AdicionarTarefaCommand("", null);

            //act 
            var result = await _handler.Handle(command, default);

            //assert
            result.Should().BeNull();
            _notificador.TemNotificacoes().Should().BeTrue();
        }

        [Fact(DisplayName = "Atualizar tarefa válida")]
        [Trait("Application", "Commands")]
        public async Task Handle_DeveRetornarTarefa_QuandoAtualizarCommandValido()
        {
            //arrange
            var tarefa = new Tarefa("Titulo", null);
            var command = new AtualizarTarefaCommand(0, "Titulo Atualizado", Status.EmProgresso, null, null);

            _repositoryMock.Setup(c => c.ObterPorIdAsync(tarefa.Id)).ReturnsAsync(tarefa);
            _repositoryMock.Setup(c => c.UnitOfWork.CommitAsync()).ReturnsAsync(true);

            //act 
            var result = await _handler.Handle(command, default);

            //assert 
            result.Should().BeOfType<Tarefa>();
            result.Titulo.Should().Be("Titulo Atualizado");
            _repositoryMock.Verify(c => c.Atualizar(tarefa), Times.Once);
        }

        [Fact(DisplayName = "Atualizar tarefa inexistente")]
        [Trait("Application", "Commands")]
        public async Task Handle_DeveRetornarNull_QuandoAtualizarTarefaNaoEncontrado()
        {
            //arrange
            var command = new AtualizarTarefaCommand(0, "Titulo Atualizado", Status.EmProgresso, null, null);

            _repositoryMock.Setup(c => c.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync((Tarefa)null);

            //act 
            var result = await _handler.Handle(command, default);

            //assert 
            result.Should().BeNull();
            _notificador.TemNotificacoes().Should().BeTrue();
        }

        [Fact(DisplayName = "Atualizar tarefa concluida")]
        [Trait("Application", "Commands")]
        public async Task Handle_DeveRetornarNull_QuandoAtualizarTarefaConcluida()
        {
            //arrange
            var tarefa = new Tarefa("Tarefa", null) { Status = Status.Concluida };
            var command = new AtualizarTarefaCommand(tarefa.Id, "Titulo Atualizado", Status.Concluida, null, null);

            _repositoryMock.Setup(c => c.ObterPorIdAsync(tarefa.Id)).ReturnsAsync(tarefa);

            //act 
            var result = await _handler.Handle(command, default);

            //assert 
            result.Should().BeNull();
            _notificador.TemNotificacoes().Should().BeTrue();
        }

        [Fact(DisplayName = "Atualizar tarefa inválida")]
        [Trait("Application", "Commands")]
        public async Task Handle_DeveRetornarNull_QuandoTarefaInvalida()
        {
            //arrange
            var tarefa = new Tarefa("Tarefa", null);
            var command = new AtualizarTarefaCommand(tarefa.Id, "Titulo Atualizado", Status.Concluida, null, null);

            _repositoryMock.Setup(c => c.ObterPorIdAsync(tarefa.Id)).ReturnsAsync(tarefa);

            //act 
            var result = await _handler.Handle(command, default);

            //assert 
            result.Should().BeNull();
            _notificador.TemNotificacoes().Should().BeTrue();
        }

        [Fact(DisplayName = "Remover tarefa não encontrada")]
        [Trait("Application", "Commands")]
        public async Task Handle_DeveRetornarNull_QuandoTarefaNaoEncontrada()
        {
            //arrange
            var command = new RemoverTarefaCommand(0);

            _repositoryMock.Setup(c => c.ObterPorIdAsync(0)).ReturnsAsync((Tarefa)null);

            //act 
            var result = await _handler.Handle(command, default);

            //assert 
            result.Should().BeNull();
            _notificador.TemNotificacoes().Should().BeTrue();
        }

        [Fact(DisplayName = "Remover tarefa válida")]
        [Trait("Application", "Commands")]
        public async Task Handle_DeveRetornarTarefa_QuandoTarefaExcluida()
        {
            //arrange
            var tarefa = new Tarefa("Tarefa", null);
            var command = new RemoverTarefaCommand(tarefa.Id);
            _repositoryMock.Setup(c => c.ObterPorIdAsync(tarefa.Id)).ReturnsAsync(tarefa);
            _repositoryMock.Setup(c => c.UnitOfWork.CommitAsync()).ReturnsAsync(true);

            //act 
            var result = await _handler.Handle(command, default);

            //assert 
            result.Should().BeOfType<Tarefa>();
            _notificador.TemNotificacoes().Should().BeFalse();
            _repositoryMock.Verify(c => c.Remover(tarefa), Times.Once);
        }
    }
}
