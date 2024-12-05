using BookCatalogBackend.Repository;

namespace BookCatalogBackend
{
    public class Program
    {
        public static void Main(string[] args)
        { 

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();

            builder.Services.AddControllers(); // Добавление поддержки контроллеров

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.MapControllers(); // Настройка маршрутов для контроллеров

            app.Run();
        }
    }
}