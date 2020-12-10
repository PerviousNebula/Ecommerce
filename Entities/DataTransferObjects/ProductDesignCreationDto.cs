using System.ComponentModel.DataAnnotations;

public class ProductDesignCreationDto
{
    [Required(ErrorMessage = "DesignConfig is required")]
    public string designConfig { get; set; }

    [Required(ErrorMessage = "DesignAsset is required")]
    public string designAsset { get; set; }

    [Required(ErrorMessage = "ProductId is required")]
    public int productId { get; set; }

    [Required(ErrorMessage = "ColorId is required")]
    public int colorId { get; set; }

    [Required(ErrorMessage = "SizeId is required")]
    public int sizeId { get; set; }
}