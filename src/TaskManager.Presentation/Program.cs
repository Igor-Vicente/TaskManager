using TaskManager.Presentation.Configuration;

namespace TaskManager.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddModelStateBehavior();
            builder.Services.AddApplicationDbContext(builder.Configuration);
            builder.Services.AddDependencies();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCorsPolicy();

            var app = builder.Build();

            app.UseMiddleware<MiddlewareException>();
            app.UseCors(app.Environment.EnvironmentName);
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
