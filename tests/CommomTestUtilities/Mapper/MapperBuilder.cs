using AutoMapper;
using MyRecipeBook.Application.Services.Automapper;

namespace CommomTestUtilities.Mapper;
public class MapperBuilder
{
    public static IMapper Build()
    {
        return new MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();
    }
}
