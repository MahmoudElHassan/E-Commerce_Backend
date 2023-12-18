using Microsoft.OpenApi.Models;

namespace E-Commerce_Backend;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(z =>
        {
            z.SwaggerDoc("v1", new OpenApiInfo { Title = "SkiNet API", Version = "v1" });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "SkiNet API v1"); });
        
        return app;
    }
}
