namespace TestTaskForCamp.WebApi.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException()
    {}
    
    public InvalidEmailException(string message) : base(message)
    {}
}