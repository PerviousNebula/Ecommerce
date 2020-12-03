using System.Collections.Generic;

public class CategoryDto
{
    public int categoryId { get; set; }
    public string name { get; set; }
    public bool archive { get; set; }

    public ICollection<ProductDto> Products { get; set; }
}
