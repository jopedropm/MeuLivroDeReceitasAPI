using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure;

//Substitui vários builder.Services.AddScoped<>
public static class DependencyInjectionExtension
{

    //É a funçao que vai ser chamada no program.cs builder.Services.AddInfrastructure
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddDbContext_SqlServer(services);
        AddRepositories(services);
    }

    private static void AddDbContext_SqlServer(IServiceCollection services)
    {
        var connectionString = "Server=NSVT-ALN13\\SQLEXPRESS;Database=DB_MeuLivroDeReceitas;User Id=sa;Password=123456;Trusted_Connection=true;Encrypt=True;TrustServerCertificate=true";

        services.AddDbContext<MyRecipeBookDBContext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
    }
}
