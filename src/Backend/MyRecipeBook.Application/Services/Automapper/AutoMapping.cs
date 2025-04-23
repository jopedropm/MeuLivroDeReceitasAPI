using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Services.Automapper;

//Profile é um perfil do AutoMapper(pacote nuget instalado)
public class AutoMapping : Profile
{
    //Construtor
    public AutoMapping()
    {
        RequestToDomain();
    }

    private void RequestToDomain()
    {
        //CreateMap<de onde vem os dados, para onde vai os dados>, a senha precisamos criptografar antes então nao vamos mapea-la
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
    }
}
