using DotNetEnv;
using fosku_server.Data;
using fosku_server.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace fosku_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetEnv.Env.Load();

            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            AddAuthentitication(builder);

            var app = builder.Build();

            ConfigureMiddleware(app);

            ConfigureEndpoints(app);

            ApplyMigrations(app);

            app.Run();
        }

        private static void ConfigureEndpoints(WebApplication app)
        {
            app.MapGet("/", (HttpContext context) =>
            {
                return "OK, but wrong link.";
            });

            app.MapGet("/login", async (CustomerLoginModel login, AppDbContext db) =>
            {
                try
                {
                    db.Customers.First(x => x.Email == login.Email && x.PasswordHash == login.PasswordHash);
                }
                catch (InvalidOperationException)
                {
                    return Results.Unauthorized();
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Env.GetString("JWT_SECRET_KEY"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, login.Email)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Results.Ok(new { Token = tokenString });
            });

            app.MapPost("/login", async (CustomerLoginModel login, AppDbContext db) =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Env.GetString("JWT_SECRET_KEY"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, login.Email)
                    }),
                    Expires = DateTime.UtcNow.AddYears(200),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Results.Ok(new { Token = tokenString });
            });

            app.MapGet("/customers", [Authorize] async (AppDbContext db) => await db.Customers.ToListAsync());

            app.MapGet("/customers/{id}", [Authorize] async (int id, AppDbContext db) =>
            {
                return await db.Customers.FirstAsync(x => x.Id == id);
            });

            app.MapPost("/customers", [Authorize] async (Customer customer, AppDbContext db) =>
            {
                var handle = db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/categories", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/categories", [Authorize] async (Category category, AppDbContext db) => 
            {
                var handle = db.Categories.Add(category);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/orders", [Authorize] async (AppDbContext db) => await db.Orders.ToListAsync());

            app.MapPost("/orders", [Authorize] async (Order order, AppDbContext db) =>
            {
                var handle = db.Orders.Add(order);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/orderitems", [Authorize] async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/orderitems", [Authorize] async (OrderItem orderItem, AppDbContext db) =>
            {
                var handle = db.OrderItems.Add(orderItem);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/paymentmethods", [Authorize] async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/paymentmethods", [Authorize] async (PaymentMethod pm, AppDbContext db) =>
            {
                var handle = db.PaymentMethods.Add(pm);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/products", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/products", [Authorize] async (Product p, AppDbContext db) =>
            {
                var handle = db.Products.Add(p);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/productimages", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/productimages", [Authorize] async (ProductImage pi, AppDbContext db) =>
            {
                var handle = db.ProductImages.Add(pi);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/reviews", async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/reviews", [Authorize] async (Review review, AppDbContext db) =>
            {
                var handle = db.Reviews.Add(review);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/shoppingcarts", [Authorize] async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/shoppingcarts", [Authorize] async (ShoppingCart sp, AppDbContext db) =>
            {
                var handle = db.ShoppingCarts.Add(sp);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{handle.Entity.Id}", handle.Entity);
            });

            app.MapGet("/shoppingcartitems", [Authorize] async (AppDbContext db) => await db.Categories.ToListAsync());

            app.MapPost("/shoppingcartitems", [Authorize] async (ShoppingCartItem spitem, AppDbContext db) =>
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
                options.UseNpgsql(Env.GetString("ConnectionStrings__DefaultConnection")));
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            app.UseStaticFiles();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();
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
