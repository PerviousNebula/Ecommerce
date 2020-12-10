using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("OrderDetail")]
    public class OrderDetail: IEntity
    {
        [Key]
        [Column("orderDetailId")]
        public int id { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int quantity { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        public int weight { get; set; }

        [Required(ErrorMessage = "Total is required")]
        public double total { get; set; }

        [ForeignKey(nameof(ProductDesign))]
        [Required(ErrorMessage = "ProductDesignId is required")]
        public int productDesignId { get; set; }
        public ProductDesign ProductDesign { get; set; }

        [ForeignKey(nameof(Order))]
        [Required(ErrorMessage = "OrderId is required")]
        public int orderId { get; set; }
        public Order Order { get; set; }
    }
}