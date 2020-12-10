using System.ComponentModel.DataAnnotations;

public class OrderDetailCreationDto
{
    [Required(ErrorMessage = "Quantity is required")]
    public int quantity { get; set; }

    [Required(ErrorMessage = "Weight is required")]
    public int weight { get; set; }

    [Required(ErrorMessage = "OrderId is required")]
    public int orderId { get; set; }

    [Required(ErrorMessage = "ProductDesign is required")]
    public ProductDesignCreationDto ProductDesign { get; set; }
}