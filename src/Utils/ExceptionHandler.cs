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

    private static ExceptionHandler FileException(string message, int errorCode)
    {
        return new ExceptionHandler($"File processing error: {message}", errorCode);
    }

    public static ExceptionHandler FileNotFound(string? message = null)
    {
        if (message is not null) return ExceptionHandler.FileException($"File with that path does not exist: {message}", 1);
        return ExceptionHandler.FileException("File with that path does not exist", 1);
    }

    public static ExceptionHandler FileInUse(string? message = null)
    {
        if (message is not null) return ExceptionHandler.FileException($"File is already in use: {message}", 2);
        return ExceptionHandler.FileException("File is already in use", 2);
    }

    private static ExceptionHandler DatabaseException(string message, int errorCode)
    {
        return new ExceptionHandler($"Database error: {message}", errorCode);
    }

    public static ExceptionHandler EmailExists(string? message = null)
    {
        if (message is not null) return ExceptionHandler.DatabaseException($"Email already in use: {message}", 1);
        return ExceptionHandler.DatabaseException("Email already in use", 1);
    }

    public static ExceptionHandler CustomerNotCreated(string? message = null)
    {
        if (message is not null) return ExceptionHandler.DatabaseException($"Failed to create customer: {message}", 2);
        return ExceptionHandler.DatabaseException("Failed to create customer", 2);
    }

    public static ExceptionHandler CustomerNotFound(string? message = null)
    {
        if (message is not null) return ExceptionHandler.DatabaseException($"Customer not found: {message}", 3);
        return ExceptionHandler.DatabaseException("Customer not found", 3);
    }

    public override string ToString()
    {
        return $"{_message}; Code: {_errorCode}";
    }
}