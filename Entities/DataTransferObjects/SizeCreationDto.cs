using System.ComponentModel.DataAnnotations;

public class SizeCreationDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
    public string name { get; set; }

    public double priceIncrement { get; set; }

    [Required(ErrorMessage = "Archive is required")]
    public bool archive { get; set; }

    [Required(ErrorMessage = "ProductId is required")]
    public int productId { get; set; }
}