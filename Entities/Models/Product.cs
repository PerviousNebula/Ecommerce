using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Product")]
    public class Product: IEntity
    {
        [Key]
        [Column("productId")]
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string name { get; set; }

        [StringLength(45, ErrorMessage = "SKU can't be longer than 45 characters")]
        public string sku { get; set; }

        [Required(ErrorMessage = "ShortDescription is required")]
        [StringLength(100, ErrorMessage = "ShortDescription can't be longer than 100 characters")]
        public string shortDescription { get; set; }

        [StringLength(100, ErrorMessage = "LongDescription can't be longer than 100 characters")]
        public string longDescription { get; set; }

        public byte[] frontCover { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double price { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        public int stock { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        public double weight { get; set; }

        [Required(ErrorMessage = "Archive is required")]
        public bool archive { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
        [ForeignKey(nameof(Category))]
        public int categoryId { get; set; }
        public Category Category { get; set; }
    }
}