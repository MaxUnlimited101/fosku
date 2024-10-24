using DotNetEnv;
using fosku_server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using fosku_server.Services.Customers;
using fosku_server.Services.Auth;
using fosku_server.Helpers;
using fosku_server.Helpers.Validation;

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

            app.Run();
        }

        // private static void AddAuthentitication(WebApplicationBuilder builder)
        // {
        //     // Add JWT authentication services
        //     var key = Encoding.ASCII.GetBytes(Env.GetString("JWT_SECRET_KEY"));
        //     builder.Services.AddAuthentication(options =>
        //     {
        //         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //     })
        //     .AddJwtBearer(options =>
        //     {
        //         options.TokenValidationParameters = new TokenValidationParameters
        //         {
        //             ValidateIssuerSigningKey = true,
        //             IssuerSigningKey = new SymmetricSecurityKey(key),
        //             ValidateIssuer = false,
        //             ValidateAudience = false
        //         };
        //     });
        // }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
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

            //TODO: add all other services (for all other Models)
            throw new Exception("Not all services addded");

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

            builder.Services.AddHttpLogging(options =>
                {
                    options.LoggingFields = HttpLoggingFields.All;
                    options.RequestHeaders.Add("Referer");
                    options.ResponseHeaders.Add("FoskuResponseHeader"); // ?
                });
        }

        private static WebApplication ConfigureMiddleware(WebApplicationBuilder builder)
        {
            // app.UseStaticFiles();
            // app.UseCors();
            // app.UseAuthentication();
            // app.UseAuthorization();

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
    }
}
