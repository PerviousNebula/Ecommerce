using System;
using System.ComponentModel.DataAnnotations;

public class UserCreationDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
    public string name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters")]
    [EmailAddress(ErrorMessage = "Invaid email address")]
    public string email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string password { get; set; }

    public byte[] passwordHash { get; set; }

    public byte[] passwordSalt { get; set; }

    public string imgSrc { get; set; }

    public DateTime createdAt { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Archive is required")]
    public bool archive { get; set; }

    [Required(ErrorMessage = "rolId is required")]
    public int rolId { get; set; }
}