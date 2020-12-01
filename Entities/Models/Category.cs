using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Category")]
    public class Category: IEntity
    {
        [Key]
        [Column("categoryId")]
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string name { get; set; }

        [Required(ErrorMessage = "Archive is required")]
        public bool archive { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}