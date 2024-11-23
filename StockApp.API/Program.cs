using Microsoft.IdentityModel.Tokens;
using StockApp.Infra.IoC;
using Microsoft.Extensions.Caching.StackExchangeRedis;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddInfrastructureAPI(builder.Configuration);


        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateAudience = false
           };
        });


        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
                policy.RequireRole("Admin"));
        });


        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();




        // Middleware para Rate Limiting

        var requestCounts = new Dictionary<string, int>();
        var resetTime = DateTime.UtcNow.AddMinutes(2);
        var request_limit = 2;

        app.Use(async (context, next) =>
        {
            var clienteIp = context.Connection.RemoteIpAddress?.ToString()?? "unknowm";

            if (!requestCounts.ContainsKey(clienteIp))
            {
                requestCounts[clienteIp] = 0;
            }
            if (DateTime.UtcNow > resetTime)
            {
                requestCounts.Clear();
                resetTime = DateTime.UtcNow.AddMinutes(2);
            }
            requestCounts[clienteIp]++;

            if (requestCounts[clienteIp]> request_limit)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Limite de requisições, tente novamente maus tarde");
                return;
            }
            await next();
        });

        //Middleware de Autorização Baseada em Roles

       


    }
}