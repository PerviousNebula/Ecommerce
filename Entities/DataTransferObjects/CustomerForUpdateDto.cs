using System.ComponentModel.DataAnnotations;

public class CustomerForUpdateDto
{
    [Required(ErrorMessage = "Customer id is required")]
    public int customerId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
    public string name { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, ErrorMessage = "Last name can't be longer than 100 characters")]
    public string lastName { get; set; }

    [Required(ErrorMessage = "email is required")]
    [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters")]
    public string email { get; set; }

    public string password { get; set; }

    public byte[] passwordHash { get; set; }

    public byte[] passwordSalt { get; set; }

    public bool archive { get; set; }
}