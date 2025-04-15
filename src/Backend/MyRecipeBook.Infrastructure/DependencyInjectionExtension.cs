using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure;

//Substitui vários builder.Services.AddScoped<>
public static class DependencyInjectionExtension
{

    //É a funçao que vai ser chamada no program.cs builder.Services.AddInfrastructure
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        AddDbContext_SqlServer(services, configuration);
        AddRepositories(services);
    }

    //Faz a conexão com o banco de dados especificado no appsettings.json
    private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddDbContext<MyRecipeBookDBContext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        //Adiciona ao DB
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        //Verifica se o email a ser registrado ja esta no DB
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();

        //Salvar no DB
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
