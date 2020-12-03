public class ProductParameters : QueryStringParameters
{
    public string name { get; set; }
    public string sku { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public int? MaxStock { get; set; }
    public double? MaxWeight { get; set; }
    public bool? archive { get; set; }
    public int? categoryId { get; set; }

    public bool ValidaPriceRange => MinPrice == null || MaxPrice == null || MinPrice < MaxPrice;
}