public class ProductDesignDto
{
    public int productDesignId { get; set; }
    public string designConfig { get; set; }
    public string designAsset { get; set; }
    public int productId { get; set; }
    public ProductDto Product { get; set; }
    public int colorId { get; set; }
    public ColorDto Color { get; set; }
    public int sizeId { get; set; }
    public SizeDto Size { get; set; }
}