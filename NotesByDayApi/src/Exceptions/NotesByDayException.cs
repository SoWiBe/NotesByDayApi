namespace NotesByDayApi.Exceptions;

public class NotesByDayException : Exception
{
    public int StatusCode { get; }
    public string ErrorType { get; }

    public NotesByDayException(string message, int statusCode = 500, string errorType = "GeneralError") 
        : base(message)
    {
        StatusCode = statusCode;
        ErrorType = errorType;
    }
}