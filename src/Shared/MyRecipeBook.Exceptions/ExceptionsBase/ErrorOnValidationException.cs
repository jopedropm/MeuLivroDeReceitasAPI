namespace MyRecipeBook.Exceptions.ExceptionsBase;
public class ErrorOnValidationException : Exception
{
    public IList<string> ErrorsMessages { get; set; }

    public ErrorOnValidationException(IList<string> errorsMessages)
    {
        ErrorsMessages = errorsMessages;
    }
}
