using fosku_server.Data;
using Microsoft.EntityFrameworkCore;

namespace fosku_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration["ConnectionStrings__DefaultConnection"]));

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseCors();

            app.MapGet("/", async (HttpContext context) =>
            {
                await context.Response.WriteAsync("OK, but wrong link.");
            });

            app.Run();
        }
    }
}
