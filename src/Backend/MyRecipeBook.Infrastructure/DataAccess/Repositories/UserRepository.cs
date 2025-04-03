namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;
public class UserRepository
{
    private readonly MyRecipeBookDBContext _dbContext;

    public UserRepository(MyRecipeBookDBContext dbContext) => _dbContext = dbContext;


}
