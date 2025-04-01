using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        //Recebe a requisição e envia a resposta sobre a regra de negócio
        public ResponseRegisterUserJson Execute(RequestRegisterUserJson request)
        {
            ValidateUser(request);

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
