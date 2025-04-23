using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Infrastructure.DataAccess;
public class UnitOfWork : IUnitOfWork
{
    //Variavel privada do construtor
    private readonly MyRecipeBookDBContext _dbContext;
    //Construtor
    public UnitOfWork(MyRecipeBookDBContext dbContext) => _dbContext = dbContext;

    //Salvar no DB
    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
