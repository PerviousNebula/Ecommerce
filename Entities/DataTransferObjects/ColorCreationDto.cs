using System.ComponentModel.DataAnnotations;

public class ColorCreationDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
    public string name { get; set; }

    [Required(ErrorMessage = "HexColor is required")]
    [StringLength(45, ErrorMessage = "HexColor can't be longer than 45 characters")]
    public string hexColor { get; set; }

    public double priceIncrement { get; set; }

    [Required(ErrorMessage = "Archive is required")]
    public bool archive { get; set; }

    [Required(ErrorMessage = "ProductId is required")]
    public int productId { get; set; }
}