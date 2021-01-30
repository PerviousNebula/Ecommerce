using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Rol")]
    public class Rol: IEntity
    {
        [Key]
        [Column("rolId")]
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
        public string name { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<RolMenu> RolMenus { get; set; }
    }
}