using MyRecipeBook.Application.Services.Automapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;

        //Recebe a requisição e envia a resposta sobre a regra de negócio
        public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
        {
            //Chama a funçao para validar as UseCases
            ValidateUser(request);

            //Criando o usuário com AutoMapper de forma manual, mas depois será feito por injeção de dependência
            var autoMapper = new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper();

            var user = autoMapper.Map<Domain.Entities.User>(request);

            //Critografia da senha
            var passwordCryptography = new PasswordEncripter();

            user.Password = passwordCryptography.Encrypt(request.Password);

            //Adcionar no banco de dados
            await _writeOnlyRepository.Add(user);

            //Retorna uma resposta do servidor
            return new ResponseRegisterUserJson
            {
                Name = request.Name
            };
        }

        private void ValidateUser(RequestRegisterUserJson request)
        {
            //Recebe as validações
            var validator = new RegisterUserValidator();

            //Passa o Validate(funçao do FluentValidator)
            var result = validator.Validate(request);

            //Se a validação nao for valida
            if (result.IsValid == false)
            {
                //Erros do FluentValidation passados no RegisterUserValidator 
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
