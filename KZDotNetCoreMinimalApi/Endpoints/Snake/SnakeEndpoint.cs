using KZDotNetCore.Domain.Features.Snake;
using KZDotNetCore.Domain.Models.Snake;

namespace KZDotNetCoreMinimalApi.Endpoints.Snake
{
    public static class SnakeEndpoint
    {
        public static void ConfigureTodoService(this WebApplicationBuilder builder)
        {
            //if you run EntityFramework, open this code
            builder.Services.AddScoped<ISnakeService, SnakeEfService>();

            //if you run AdoDotNet, open this code
            //builder.Services.AddScoped<ISnakeService, SnakeAdoDotNetService>(); 

            //if you run Dapper, open this code
            //builder.Services.AddScoped<ISnakeService, SnakeDapperService>();
        }
        public static void UseSnakeEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/snake", async (ISnakeService snakeService) =>
            {
                return Results.Ok(await snakeService.Get());
            })
            .WithName("GetSnake")
            .WithOpenApi();

            app.MapGet("/api/snake/{id}", async (int id, ISnakeService snakeService) =>
            {
                var item = await snakeService.GetByID(id);
                
                return item.Response.IsSuccess ? Results.Ok(item) : Results.BadRequest(item);
            })
            .WithName("GetSnakeByID")
            .WithOpenApi();

            app.MapPost("/api/snake", (SnakeRequestModel requestModel, ISnakeService snakeService) =>
            {
                return Results.Ok(snakeService.Create(requestModel));
            })
            .WithName("CreateSnake")
            .WithOpenApi();

            app.MapPut("/api/snake/{id}", (int id, SnakeRequestModel requestModel, ISnakeService snakeService) =>
            {
                return Results.Ok(snakeService.Update(id, requestModel));
            })
            .WithName("UpdateSnake")
            .WithOpenApi();

            app.MapDelete("/api/snake/{id}", (int id, ISnakeService snakeService) =>
            {
                return Results.Ok(snakeService.Delete(id));
            })
            .WithName("DeleteSnake")
            .WithOpenApi();
        }
    }
}
