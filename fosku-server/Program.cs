using DotNetEnv;
using fosku_server.Data;
using fosku_server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
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

        private static void ConfigureEndpoints(WebApplication app)
        {
            // may be used like a health check
            app.MapGet("/", () => Results.Ok());

            #region login & auth (not done)
            //app.MapPost("/login", async ([FromBody] CustomerLoginModel login, AppDbContext db) =>
            //{
            //    try
            //    {
            //        db.Customers.First(x => x.Email == login.Email && x.PasswordHash == login.PasswordHash);
            //    }
            //    catch (InvalidOperationException)
            //    {
            //        return Results.Unauthorized();
            //    }
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var key = Encoding.ASCII.GetBytes(Env.GetString("JWT_SECRET_KEY"));
            //    var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new Claim[]
            //        {
            //            new Claim(ClaimTypes.Email, login.Email)
            //        }),
            //        Expires = DateTime.UtcNow.AddHours(1),
            //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //    };
            //    var token = tokenHandler.CreateToken(tokenDescriptor);
            //    var tokenString = tokenHandler.WriteToken(token);

            //    return Results.Ok(new { Token = tokenString });
            //});

            //app.MapPost("/signup", async ([FromBody] CustomerLoginModel login, AppDbContext db) =>
            //{
            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var key = Encoding.ASCII.GetBytes(Env.GetString("JWT_SECRET_KEY"));
            //    var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new Claim[]
            //        {
            //            new Claim(ClaimTypes.Email, login.Email)
            //        }),
            //        Expires = DateTime.UtcNow.AddYears(200),
            //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //    };
            //    var token = tokenHandler.CreateToken(tokenDescriptor);
            //    var tokenString = tokenHandler.WriteToken(token);

            //    return Results.Ok(new { Token = tokenString });
            //});
            #endregion

            app.MapGet("/customers", async (AppDbContext db) => await db.Customers.ToListAsync());

            app.MapGet("/customers/{id}", async (int id, AppDbContext db) =>
            {
                // TODO: fix for customer not found
                return await db.Customers.FirstAsync(x => x.Id == id);
            });

            app.MapPost("/customers", async (Customer customer, AppDbContext db) =>
            {
                var handle = db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/categories", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/categories", async (Category category, AppDbContext db) =>
            {
                var handle = db.Categories.Add(category);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/orders", async (AppDbContext db) => await db.Orders.ToListAsync());

            app.MapPost("/orders", async (Order order, AppDbContext db) =>
            {
                var handle = db.Orders.Add(order);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/orderitems", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/orderitems", async (OrderItem orderItem, AppDbContext db) =>
            {
                var handle = db.OrderItems.Add(orderItem);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/paymentmethods", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/paymentmethods", async (PaymentMethod pm, AppDbContext db) =>
            {
                var handle = db.PaymentMethods.Add(pm);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/products", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/products", async (Product p, AppDbContext db) =>
            {
                var handle = db.Products.Add(p);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/productimages", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/productimages", async (ProductImage pi, AppDbContext db) =>
            {
                var handle = db.ProductImages.Add(pi);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/reviews", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/reviews", async (Review review, AppDbContext db) =>
            {
                var handle = db.Reviews.Add(review);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/shoppingcarts", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/shoppingcarts", async (ShoppingCart sp, AppDbContext db) =>
            {
                var handle = db.ShoppingCarts.Add(sp);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/shoppingcartitems", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/shoppingcartitems", async (ShoppingCartItem spitem, AppDbContext db) =>
            {
                var handle = db.ShoppingCartItems.Add(spitem);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });
        }

        private static void AddAuthentitication(WebApplicationBuilder builder)
        {
            // Add JWT authentication services
            var key = Encoding.ASCII.GetBytes(Env.GetString("JWT_SECRET_KEY"));
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

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
