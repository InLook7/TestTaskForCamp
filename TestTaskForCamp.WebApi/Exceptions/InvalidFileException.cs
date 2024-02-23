namespace TestTaskForCamp.WebApi.Exceptions;

public class InvalidFileException : Exception
{
    public InvalidFileException()
    {}
    
    public InvalidFileException(string message) : base(message)
    {}
}