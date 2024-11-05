using KZDotNetCore.Domain.Database;
using KZDotNetCore.Domain.Features.Snake;
using KZDotNetCore.Domain.Features.Todo;
using KZDotNetCore.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//string connectionString = "Server=DESKTOP-V2EQ7QF\\MSSQLSERVER2019;Database=PIDDotNetTraining;User ID=sa;Password=sasa;TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
    
});
builder.Services.AddScoped(n => new DapperService(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddScoped(n => new AdoDotNetService(builder.Configuration.GetConnectionString("Connection")));

//builder.Services.AddScoped<ITodoServices, ToDoEfService>(); 
////builder.Services.AddScoped<ISnakeService, SnakeEfService>(); 

//builder.Services.AddScoped<ISnakeService, SnakeDapperService>();
builder.Services.AddScoped<ISnakeService, SnakeAdoDotNetService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
