using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;
public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    //Variavel privada do construtor
    private readonly MyRecipeBookDBContext _dbContext;
    //Construtor
    public UserRepository(MyRecipeBookDBContext dbContext) 
        => _dbContext = dbContext;

    //Adicionar ao banco de dados
    public async Task Add(User user) => 
        await _dbContext.Users.AddAsync(user);

    //Verificar se já existe um user com o email
    public async Task<bool> ExistActiveUserWithEmail(string email) => 
        await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
}
