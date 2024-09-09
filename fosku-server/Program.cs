using Fosku.Models;
using fosku_server.Data;
using Microsoft.EntityFrameworkCore;

namespace fosku_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            ConfigureMiddleware(app);

            Thread.Sleep(1000); // wait for the DB

            ApplyMigrations(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(DotNetEnv.Env.GetString("ConnectionStrings__DefaultConnection")));
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            app.UseStaticFiles();

            app.UseCors();

            app.MapGet("/", async (HttpContext context) =>
            {
                //await context.Response.WriteAsync("OK, but wrong link.");
                return "OK, but wrong link.";
            });

            app.MapGet("/users", async (AppDbContext db) =>
            {
                //using (var scope = app.Services.CreateScope())
                //{
                //    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                for (int i = 0; i < 5; i++)
                    db.Users.Add(new User
                    {
                        Address = "adress",
                        City = "city",
                        Country = "country",
                        CreatedAt = new DateTime(),
                        Email = "email",
                        FirstName = "firstname",
                        LastName = "lastname",
                        PasswordHash = "passwordhash",
                        PhoneNumber = "1234567890",
                    });
                await db.SaveChangesAsync();
                return db.Users.ToList();
                //    await context.Response.WriteAsJsonAsync(dbContext.Users.ToList());
                //    dbContext.Users.Remove(dbContext.Users.First());
                //}

            });
        }

        private static void ApplyMigrations(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
