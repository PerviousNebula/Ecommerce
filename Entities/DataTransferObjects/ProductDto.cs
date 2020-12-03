using System.Collections.Generic;

public class ProductDto
{
    public int productId { get; set; }
    public string name { get; set; }
    public string sku { get; set; }
    public string shortDescription { get; set; }
    public string longDescription { get; set; }
    public byte[] frontCover { get; set; }
    public double price { get; set; }
    public int stock { get; set; }
    public double weight { get; set; }
    public bool archive { get; set; }
    public int categoryId { get; set; }

    public ICollection<SizeDto> Sizes { get; set; }

    public ICollection<ColorDto> Colors { get; set; }

}