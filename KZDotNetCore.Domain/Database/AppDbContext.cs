using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KZDotNetCore.Domain.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TodoDataModel> Todos { get; set; }

}
