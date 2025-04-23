using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyRecipeBook.Exceptions.ExceptionsBase;

//Erro de validação do usecase
public class ErrorOnValidationException : MyRecipeBookException
{
    public IList<string> ErrorsMessages { get; set; }

    public ErrorOnValidationException(IList<string> errorsMessages)
    {
        ErrorsMessages = errorsMessages;
    }
}
