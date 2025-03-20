
using MediatR;
using TaskManager.Domain.Entities;

namespace TaskManager.Application
{
    public abstract class Command : IRequest<Tarefa>
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
