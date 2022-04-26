using Xero.Products.App.Middleware;
using Microsoft.EntityFrameworkCore;
using Xero.Products.DataLayer.Repositories;
using Xero.Products.DataLayer.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Xero.Products.App.Authentication;
using Microsoft.AspNetCore.Builder;

namespace Xero.Products.App;

public class Startup
{

    readonly IConfiguration _config;
    readonly IWebHostEnvironment _environment;

    public Startup(IConfiguration config, IWebHostEnvironment environment)
    {
        _config = config;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSecret"]))
            };
        });

        services.AddDbContext<ProductsContext>
            (options => options.UseSqlite("Name=ProductsDB"), ServiceLifetime.Singleton);

        services.AddSingleton<IProductRepository, ProductRepository>();
        services.AddSingleton<IProductOptionsRepository, ProductOptionsRepository>();
        services.AddSingleton<IProductService, ProductService>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITokenService, TokenService>();
    }
    public void Configure(IApplicationBuilder app)
    {
        // Configure the HTTP request pipeline.
        if (_environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<ErrorHandlerMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
