using TaskManager.Domain.Entities;
using TaskManager.Presentation.ViewModels;

namespace TaskManager.Presentation.Configuration
{
    public static class ModelConvertExtensions
    {
        public static ResponseTarefaViewModel ToResponseTarefaViewModel(this Tarefa tarefa)
        {
            return new ResponseTarefaViewModel
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status,
                CriadaEm = tarefa.CriadaEm,
                ConcluidaEm = tarefa.ConcluidaEm
            };
        }

        public static IEnumerable<ResponseTarefaViewModel> ToResponseTarefaViewModel(this IEnumerable<Tarefa> tarefas)
        {
            List<ResponseTarefaViewModel> model = new();

            foreach (var tarefa in tarefas) model.Add(tarefa.ToResponseTarefaViewModel());

            return model;
        }
    }
}
