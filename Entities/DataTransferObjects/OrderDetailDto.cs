public class OrderDetailDto
{
    public int orderDetailId { get; set; }
    public int quantity { get; set; }
    public int weight { get; set; }
    public double total { get; set; }
    public ProductDesignDto ProductDesign { get; set; }
}