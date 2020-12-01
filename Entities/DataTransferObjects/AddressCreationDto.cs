using System.ComponentModel.DataAnnotations;

public class AddressCreationDto
{
    [Required(ErrorMessage = "Address1 is required")]
    [StringLength(200, ErrorMessage = "Address1 can't be longer than 200 characters")]
    public string address1 { get; set; }

    [StringLength(100, ErrorMessage = "Address2 can't be longer than 100 characters")]
    public string address2 { get; set; }

    [Required(ErrorMessage = "City is required")]
    [StringLength(45, ErrorMessage = "City can't be longer than 45 characters")]
    public string city { get; set; }

    [Required(ErrorMessage = "State is required")]
    [StringLength(45, ErrorMessage = "State can't be longer than 45 characters")]
    public string state { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [StringLength(45, ErrorMessage = "Country can't be longer than 45 characters")]
    public string country { get; set; }

    [Required(ErrorMessage = "Zip is required")]
    [StringLength(10, ErrorMessage = "Zip can't be longer than 10 characters")]
    public string zip { get; set; }

    [Required(ErrorMessage = "Archive is required")]
    public bool archive { get; set; }

    [Required(ErrorMessage = "CustomerId is required")]
    public int customerId { get; set; }
}