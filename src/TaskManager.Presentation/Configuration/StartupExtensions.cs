using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Commands;
using TaskManager.Application.Queries;
using TaskManager.Domain.Abstractions;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Data.Repository;

namespace TaskManager.Presentation.Configuration
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            return services;
        }

        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AdicionarTarefaCommand, Tarefa>, TarefaCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarTarefaCommand, Tarefa>, TarefaCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverTarefaCommand, Tarefa>, TarefaCommandHandler>();

            services.AddScoped<ITarefaQueries, TarefaQueries>();

            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<ITarefaRepository, TarefaRepository>();

            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Development", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });

                options.AddPolicy("Production", policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
            });

            return services;
        }

        public static IServiceCollection AddModelStateBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            return services;
        }
    }
}
