using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Menu")]
    public class Menu : IEntity
    {
        [Key]
        [Column("menuId")]
        public int id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(45, ErrorMessage = "Title can't be longer than 45 characters")]
        public string title { get; set; }

        [Required(ErrorMessage = "Icon is required")]
        [StringLength(45, ErrorMessage = "Icon can't be longer than 45 characters")]
        public string icon { get; set; }

        [Required(ErrorMessage = "Url is required")]
        [StringLength(45, ErrorMessage = "Url can't be longer than 45 characters")]
        public string url { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }

        public ICollection<RolMenu> RolMenus { get; set; }
    }
}