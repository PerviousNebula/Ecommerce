using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class OrderForUpdateDto
    {
        [Required(ErrorMessage = "OrderId is required")]
        public int orderId { get; set; }

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

        public string discountCode { get; set; }

        public int? customerId { get; set; }

        public int? addressId { get; set; }
    }
}