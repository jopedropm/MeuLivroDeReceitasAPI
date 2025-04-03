using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.DataAccess;
public class MyRecipeBookDBContext : DbContext
{
    //Construtor
    public MyRecipeBookDBContext(DbContextOptions options) : base(options)
    {
    }

    //Variavel de usuarios
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDBContext).Assembly);
    }
}
