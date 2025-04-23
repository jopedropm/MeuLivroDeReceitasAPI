using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Envia dados do cliente para o servidor
        [HttpPost]
        //Gera o statuscodes 201 de Created
        [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]

        //Funçao de EndPoint
        public async Task<IActionResult> Register(
            RequestRegisterUserJson request,
            IRegisterUserUseCase useCase)
        {
            //Executa a regra de negócio com a função execute do RegisterUserUseCase
            var result = await useCase.Execute(request);

            //Passa para o IActionResult o metodo Created (201)
            return Created(string.Empty, result);
        }
    }
}