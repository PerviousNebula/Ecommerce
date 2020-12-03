using System.ComponentModel.DataAnnotations;

public class ColorForUpdateDto
{
    [Required(ErrorMessage = "ColorId is required")]
    public int colorId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
    public string name { get; set; }

    [Required(ErrorMessage = "HexColor is required")]
    [StringLength(45, ErrorMessage = "HexColor can't be longer than 45 characters")]
    public string hexColor { get; set; }

    public double priceIncrement { get; set; }

    [Required(ErrorMessage = "Archive is required")]
    public bool archive { get; set; }
}