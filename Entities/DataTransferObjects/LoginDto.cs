using System.ComponentModel.DataAnnotations;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string password { get; set; }
}