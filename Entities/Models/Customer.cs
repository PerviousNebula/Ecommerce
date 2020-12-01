using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Customer")]
    public class Customer: IEntity
    {
        [Key]
        [Column("customerId")]
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string name { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name can't be longer than 100 characters")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters")]
        public string email { get; set; }

        [Required(ErrorMessage = "PasswordHash is required")]
        public byte[] passwordHash { get; set; }

        [Required(ErrorMessage = "PasswordSalt is required")]
        public byte[] passwordSalt { get; set; }

        [Required(ErrorMessage = "SignupDate is required")]
        public DateTime signupDate { get; set; }

        [Required(ErrorMessage = "Archive is required")]
        public bool archive { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}