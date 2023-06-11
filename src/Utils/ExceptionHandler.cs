namespace Utils;

class ExceptionHandler : Exception
{
    private string _message;
    private int _errorCode;

    public ExceptionHandler(string message, int errorCode)
    {
        _message = message;
        _errorCode = errorCode;
    }

    public static ExceptionHandler FileException(string message, int errorCode)
    {
        return new ExceptionHandler($"File processing error: {message}", errorCode);
    }

    public static ExceptionHandler DatabaseException(string message, int errorCode)
    {
        return new ExceptionHandler($"Database error: {message}", errorCode);
    }

    public override string ToString()
    {
        return $"{_message}; Code: {_errorCode}";
    }
}