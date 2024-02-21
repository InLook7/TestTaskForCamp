using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace TestTaskForCamp.Blazor.Models;

public class InputDataModel
{
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*" +
                       ")(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\." +
                       ")+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$", 
        ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = ".docx file is required")]
    public IBrowserFile File { get; set; }
}