using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.Application.Commands;
using TaskManager.Application.Queries;
using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Presentation.Controllers;
using TaskManager.Presentation.ViewModels;

namespace TaskManager.Presentation.Tests.Controllers
{
    public class TarefaControllerTests
    {
        private readonly TarefaController _controller;
        private readonly INotificador _notificador;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ITarefaQueries> _queriesMock;

        public TarefaControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _queriesMock = new Mock<ITarefaQueries>();
            _notificador = new Notificador();
            _controller = new TarefaController(_mediatorMock.Object, _notificador, _queriesMock.Object);
        }

        [Fact(DisplayName = "Obter tarefa")]
        [Trait("Presentation", "Controller")]
        public async Task ObterTarefa_DeveRetornarTarefa_QuandoIdValido()
        {
            //arrange
            var tarefa = new Tarefa("x", null);
            _queriesMock.Setup(s => s.ObterTarefaAsync(tarefa.Id)).ReturnsAsync(tarefa);

            //act
            var result = await _controller.ObterTarefa(tarefa.Id);

            //assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<ResponseTarefaViewModel>();
            _queriesMock.Verify(s => s.ObterTarefaAsync(tarefa.Id), Times.Once);
        }

        [Fact(DisplayName = "Obter tarefa inexistente")]
        [Trait("Presentation", "Controller")]
        public async Task ObterTarefa_DeveNotFound_QuandoTarefaInexistente()
        {
            //arrange
            _queriesMock.Setup(s => s.ObterTarefaAsync(0)).ReturnsAsync((Tarefa)null);

            //act
            var result = await _controller.ObterTarefa(0);

            //assert
            result.Should().BeOfType<NotFoundResult>();
            _queriesMock.Verify(s => s.ObterTarefaAsync(0), Times.Once);
        }

        [Fact(DisplayName = "Obter lista de tarefas")]
        [Trait("Presentation", "Controller")]
        public async Task ObterTarefa_DeveRetornarListaTarefas_QuandoSizeMaiorQueZero()
        {
            //arrange
            var tarefas = new List<Tarefa>()
            {
                new Tarefa("z", null),
                new Tarefa("w", null)
            };

            _queriesMock.Setup(s => s.ObterTarefasAsync(1, 10)).ReturnsAsync(tarefas);

            //act
            var result = await _controller.ObterTarefas();

            //assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<ResponseTarefaViewModel>>();
            _queriesMock.Verify(s => s.ObterTarefasAsync(1, 10), Times.Once);
        }

        [Fact(DisplayName = "Adicionar tarefa válida")]
        [Trait("Presentation", "Controller")]
        public async Task AdicionarTarefa_DeveRetornarTarefa_QuandoInputValido()
        {
            //arrange
            var model = new AdicionarTarefaViewModel() { Titulo = "z" };
            var tarefa = new Tarefa(model.Titulo, model.Descricao);
            _mediatorMock.Setup(s => s.Send(It.IsAny<AdicionarTarefaCommand>(), default)).ReturnsAsync(tarefa);

            //act
            var result = await _controller.AdicionarTarefa(model);

            //assert
            result.Should().BeOfType<CreatedAtActionResult>().Which.Value.Should().BeOfType<ResponseTarefaViewModel>();
            _notificador.TemNotificacoes().Should().BeFalse();
        }

        [Fact(DisplayName = "Adicionar tarefa inválida")]
        [Trait("Presentation", "Controller")]
        public async Task AdicionarTarefa_DeveRetornarBadRequest_QuandoModelStateInvalida()
        {
            //arrange
            var model = new AdicionarTarefaViewModel();
            _controller.ModelState.AddModelError("Titulo", "Forneça um título");

            //act
            var result = await _controller.AdicionarTarefa(model);

            //assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact(DisplayName = "Adicionar tarefa inválida")]
        [Trait("Presentation", "Controller")]
        public async Task AdicionarTarefa_DeveRetornarBadRequest_QuandoTemNotificacao()
        {
            //arrange
            var model = new AdicionarTarefaViewModel();
            _notificador.AdicionarNotificacao("Forneça um titulo");

            //act
            var result = await _controller.AdicionarTarefa(model);

            //assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _notificador.TemNotificacoes().Should().BeTrue();
        }

        [Fact(DisplayName = "Atualizar tarefa inválida")]
        [Trait("Presentation", "Controller")]
        public async Task AtualizarTarefa_DeveRetornarBadRequest_QuandoModelStateInvalida()
        {
            //arrange
            var model = new AtualizarTarefaViewModel() { ConcluidaEm = DateTime.Now, Status = Domain.Enums.Status.Concluida };
            _controller.ModelState.AddModelError("Titulo", "Forneça um título");

            //act
            var result = await _controller.AtualizarTarefa(0, model);

            //assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }


        [Fact(DisplayName = "Atualizar tarefa inválida")]
        [Trait("Presentation", "Controller")]
        public async Task AtualizarTarefa_DeveRetornarBadRequest_QuandoTemNotificacao()
        {
            //arrange
            var model = new AtualizarTarefaViewModel();
            _notificador.AdicionarNotificacao("Forneça um titulo");

            //act
            var result = await _controller.AtualizarTarefa(0, model);

            //assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _notificador.TemNotificacoes().Should().BeTrue();
        }

        [Fact(DisplayName = "Atualizar tarefa válida")]
        [Trait("Presentation", "Controller")]
        public async Task AtualizarTarefa_DeveRetornarTarefa_QuandoInputValido()
        {
            //arrange
            var model = new AtualizarTarefaViewModel() { Titulo = "z" };
            var tarefa = new Tarefa(model.Titulo, model.Descricao);
            _mediatorMock.Setup(s => s.Send(It.IsAny<AtualizarTarefaCommand>(), default)).ReturnsAsync(tarefa);

            //act
            var result = await _controller.AtualizarTarefa(tarefa.Id, model);

            //assert
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<ResponseTarefaViewModel>();
            _notificador.TemNotificacoes().Should().BeFalse();
        }

        [Fact(DisplayName = "Excluir tarefa não encontrada")]
        [Trait("Presentation", "Controller")]
        public async Task ExcluirTarefa_DeveRetornarBadRequest_QuandoTemNotificacao()
        {
            //arrange
            _notificador.AdicionarNotificacao("Tarefa não encontrada");

            //act
            var result = await _controller.ExcluirTarefa(0);

            //assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _notificador.TemNotificacoes().Should().BeTrue();
        }

        [Fact(DisplayName = "Excluir tarefa não encontrada")]
        [Trait("Presentation", "Controller")]
        public async Task ExcluirTarefa_DeveRetornarNoContent_QuandoInputValido()
        {
            // act
            var result = await _controller.ExcluirTarefa(0);

            //assert
            result.Should().BeOfType<NoContentResult>();
            _notificador.TemNotificacoes().Should().BeFalse();
        }
    }
}
