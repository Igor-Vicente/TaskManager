using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskManager.Application.Commands;
using TaskManager.Application.Queries;
using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Enums;
using TaskManager.Presentation.Configuration;
using TaskManager.Presentation.ViewModels;

namespace TaskManager.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/tarefa")]
    public class TarefaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITarefaQueries _tarefaQueries;
        private readonly INotificador _notificador;

        public TarefaController(IMediator mediator, INotificador notificador, ITarefaQueries tarefaQueries)
        {
            _mediator = mediator;
            _notificador = notificador;
            _tarefaQueries = tarefaQueries;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterTarefa(int id)
        {
            var tarefa = await _tarefaQueries.ObterTarefaAsync(id);

            if (tarefa == null) return NotFound();

            return Ok(tarefa.ToResponseTarefaViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> ObterTarefas(int pgIndex = 1, int pageSize = 20, Status? status = null)
        {
            var tarefas = await _tarefaQueries.ObterTarefasAsync(Math.Max(1, pgIndex), Math.Clamp(pageSize, 1, 50), status);

            return Ok(tarefas.ToResponseTarefaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarTarefa(AdicionarTarefaViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequestResponse(ModelState);

            var tarefa = await _mediator.Send(new AdicionarTarefaCommand(model.Titulo, model.Descricao));

            if (_notificador.TemNotificacoes())
                return BadRequestResponse(_notificador);

            return CreatedAtAction(nameof(ObterTarefa), new { id = tarefa.Id }, tarefa!.ToResponseTarefaViewModel());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarTarefa(int id, AtualizarTarefaViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequestResponse(ModelState);

            var tarefa = await _mediator.Send(new AtualizarTarefaCommand(id, model.Titulo, model.Status, model.Descricao, model.ConcluidaEm));

            if (_notificador.TemNotificacoes())
                return BadRequestResponse(_notificador);

            return Ok(tarefa.ToResponseTarefaViewModel());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirTarefa(int id)
        {
            await _mediator.Send(new RemoverTarefaCommand(id));

            if (_notificador.TemNotificacoes())
                return BadRequestResponse(_notificador);

            return NoContent();
        }

        private IActionResult BadRequestResponse(ModelStateDictionary modelState) =>
            new BadRequestObjectResult(new { Sucesso = false, Erros = modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage) });
        private IActionResult BadRequestResponse(INotificador notificador) =>
            new BadRequestObjectResult(new { Sucesso = false, Erros = notificador.ObterNotificacoes() });
    }
}
