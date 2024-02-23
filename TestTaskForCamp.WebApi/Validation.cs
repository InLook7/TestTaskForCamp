using System.Text.RegularExpressions;

namespace TestTaskForCamp.WebApi;

public static class Validation
{
    private static readonly string _pattern = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*" +
                                     ")(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\." +
                                     ")+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";
    
    public static bool IsFileValid(IFormFile file)
    {
        return Path.GetExtension(file.FileName) == ".docx";
    }

    public static bool IsEmailValid(string email)
    {
        return Regex.IsMatch(email, _pattern);
    }
}