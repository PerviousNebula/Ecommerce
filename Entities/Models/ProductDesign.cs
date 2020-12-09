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
        public byte[] designConfig { get; set; }

        [Required(ErrorMessage = "DesignAsset is required")]
        public byte[] designAsset { get; set; }

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
    }
}