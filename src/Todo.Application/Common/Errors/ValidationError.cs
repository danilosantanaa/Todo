namespace Todo.Application.Common.Errors;

public class ValidationError : Exception
{
    public ValidationError(List<Error> errors)
    {
        Errors = errors;
    }
    public List<Error> Errors { get; set; }
}

public class Error
{
    public Error(string propertyName, string errorMessage)
    {
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }

    public string PropertyName { get; init; }
    public string ErrorMessage { get; init; }
}