using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("User")]
    public class User: IEntity
    {
        [Key]
        [Column("userId")]
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters")]
        public string email { get; set; }

        [Required(ErrorMessage = "PasswordHash is required")]
        public byte[] passwordHash { get; set; }

        [Required(ErrorMessage = "PasswordSalt is required")]
        public byte[] passwordSalt { get; set; }

        public byte[] imgSrc { get; set; }

        public DateTime createdAt { get; set; }

        [Required(ErrorMessage = "Archive is required")]
        public bool archive { get; set; }

        [Required(ErrorMessage = "rolId is required")]
        [ForeignKey(nameof(Rol))]
        public int rolId { get; set; }
        public Rol Rol { get; set; }
    }
}