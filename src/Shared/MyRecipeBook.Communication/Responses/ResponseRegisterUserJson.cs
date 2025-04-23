using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Communication.Responses
{
    //Corpo da resposta do StatusCodes 201
    public class ResponseRegisterUserJson
    {
        public string Name { get; set; } = string.Empty;
    }
}
