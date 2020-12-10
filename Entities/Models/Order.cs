using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Order")]
    public class Order: IEntity
    {
        [Key]
        [Column("orderId")]
        public int id { get; set; }

        public string orderNumber { get; set; }

        [StringLength(200, ErrorMessage = "Address1 can't be longer than 200 characters")]
        public string address1 { get; set; }

        [StringLength(100, ErrorMessage = "Address2 can't be longer than 100 characters")]
        public string address2 { get; set; }

        [StringLength(45, ErrorMessage = "City can't be longer than 45 characters")]
        public string city { get; set; }

        [StringLength(45, ErrorMessage = "State can't be longer than 45 characters")]
        public string state { get; set; }

        [StringLength(45, ErrorMessage = "Country can't be longer than 45 characters")]
        public string country { get; set; }

        [StringLength(20, ErrorMessage = "Zip can't be longer than 20 characters")]
        public string zip { get; set; }

        [Required(ErrorMessage = "Shipping is required")]
        public double shipping { get; set; }

        [Required(ErrorMessage = "Tax is required")]
        public double tax { get; set; }

        public double discount { get; set; }

        [Required(ErrorMessage = "Total is required")]
        public double total { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime date { get; set; }

        public bool isPaid { get; set; }

        [ForeignKey(nameof(Customer))]
        public int? customerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey(nameof(Address))]
        public int? addressId { get; set; }
        public Address Address { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}