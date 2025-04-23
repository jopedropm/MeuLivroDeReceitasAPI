namespace MyRecipeBook.Communication.Responses;

//É daqui que sai as mensagens de erros que estão em ResourceMessagesException
public class ResponseErrorJson
{
    public IList<string> Errors { get; set; }

    public ResponseErrorJson(IList<string> errors) => Errors = errors;

    public ResponseErrorJson(string error)
    {
        Errors = new List<string>()
        {
            //Errors.Add(error);
            error
        };
    }
}
