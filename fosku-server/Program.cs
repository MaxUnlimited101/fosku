using DotNetEnv;
using fosku_server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using fosku_server.Services.Auth;
using fosku_server.Helpers;
using fosku_server.Helpers.Validation;
using fosku_server.Services.Customers;
using fosku_server.Services.Orders;
using fosku_server.Services.Categories;
using fosku_server.Services.OrderItems;
using fosku_server.Services.Products;
using fosku_server.Services.ProductImages;
using fosku_server.Services.Reviews;
using Microsoft.AspNetCore.HttpLogging;
using fosku_server.Services.Managers;
using fosku_server.Models;

namespace fosku_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            var app = ConfigureMiddleware(builder);

            //ConfigureEndpoints(app); // ?

            ApplyMigrations(app);

            app.MapControllers();

            GenerateRootManager(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpLogging(options =>
            {
                options.LoggingFields = HttpLoggingFields.All;
                options.RequestHeaders.Add("Referer");
                options.ResponseHeaders.Add("FoskuResponseHeader"); // TODO: change this?
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
                options.UseNpgsql(Env.GetString("ConnectionStrings__DefaultConnection")));

            //builder.Services.AddScheduler(); // ?
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductImageService, ProductImageService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IManagerService, ManagerService>();

            //TODO: add DELETE for Category, Product, ProdcutImage, ...??
            //TODO: add unit testing ??

            builder.Services.AddTransient<IAuthService, AuthService>();

            builder.Services.AddMvc(opt =>
            {
                opt.Filters.Add<ValidationFilter>();
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["access_token"];
                            return Task.CompletedTask;
                        }
                    };
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthSettings.JWTPrivateKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            builder.Services.AddAuthorization();
        }

        private static WebApplication ConfigureMiddleware(WebApplicationBuilder builder)
        {

            var app = builder.Build();
            {
                app.UseHttpLogging();
                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseCors();
                app.UseAuthentication();
                app.UseAuthorization();
            }
            return app;
        }

        private static void ApplyMigrations(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }
        }

        private static void GenerateRootManager(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            IManagerService managerService = scope.ServiceProvider.GetRequiredService<IManagerService>();

            if (managerService.GetManager("root") == null)
            {
                Manager root = new();
                root.Email = "root";
                root.FirstName = "root";
                root.LastName = "root";
                root.PasswordHash = Env.GetString("ROOT_MANAGER_PASSWORD");
                managerService.CreateManager(root);
            }
        }
    }
}
