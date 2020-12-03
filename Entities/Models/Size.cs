using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Size")]
    public class Size: IEntity
    {
        [Key]
        [Column("sizeId")]
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string name { get; set; }

        public double priceIncrement { get; set; }

        [Required(ErrorMessage = "Archive is required")]
        public bool archive { get; set; }

        [Required(ErrorMessage = "ProductId is required")]
        [ForeignKey(nameof(Product))]
        public int productId { get; set; }
        public Product Product { get; set; }
    }
}