using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("ProductDesign")]
    public class ProductDesign: IEntity
    {
        [Key]
        [Column("productDesignId")]
        public int id { get; set; }

        [Required(ErrorMessage = "DesignConfig is required")]
        public string designConfig { get; set; }

        [Required(ErrorMessage = "DesignAsset is required")]
        public string designAsset { get; set; }

        [ForeignKey(nameof(Product))]
        [Required(ErrorMessage = "ProductId is required")]
        public int productId { get; set; }
        public Product Product { get; set; }

        [ForeignKey(nameof(Color))]
        [Required(ErrorMessage = "ColorId is required")]
        public int colorId { get; set; }
        public Color Color { get; set; }

        [ForeignKey(nameof(Size))]
        [Required(ErrorMessage = "SizeId is required")]
        public int sizeId { get; set; }
        public Size Size { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}