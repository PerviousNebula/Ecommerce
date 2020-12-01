using System;
using System.ComponentModel.DataAnnotations;

public class CustomerCreationDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
    public string name { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, ErrorMessage = "Last name can't be longer than 100 characters")]
    public string lastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters")]
    public string email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string password { get; set; }

    public byte[] passwordHash { get; set; }
    
    public byte[] passwordSalt { get; set; }

    public DateTime signupDate { get; set; } = DateTime.Now;
}