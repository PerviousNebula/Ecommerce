using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Color")]
    public class Color: IEntity
    {
        [Key]
        [Column("colorId")]
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string name { get; set; }

        [Required(ErrorMessage = "HexColor is required")]
        [StringLength(45, ErrorMessage = "HexColor can't be longer than 45 characters")]
        public string hexColor { get; set; }

        public double priceIncrement { get; set; }

        [Required(ErrorMessage = "Archive is required")]
        public bool archive { get; set; }

        [Required(ErrorMessage = "ProductId is required")]
        [ForeignKey(nameof(Product))]
        public int productId { get; set; }
        public Product Product { get; set; }
    }
}