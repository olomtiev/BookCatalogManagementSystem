using AspNetCoreRateLimit;
using BookCatalog.Repository;
using Microsoft.AspNetCore.ResponseCompression;

namespace BookCatalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Добавление политики CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()   // Разрешает любой источник
                           .AllowAnyMethod()   // Разрешает любые HTTP методы (GET, POST и т.д.)
                           .AllowAnyHeader()); // Разрешает любые заголовки
            });

            builder.Services.AddMemoryCache();
            builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
            builder.Services.AddInMemoryRateLimiting();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            // Добавление репозитория
            builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();

            // Добавление сервисов для контроллеров и страниц
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Применение CORS политики
            app.UseCors("AllowAll");
             
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
             
            app.UseIpRateLimiting();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}
