using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("RolMenu")]
    public class RolMenu : IEntity
    {
        [Key]
        [Column("rolMenuId")]
        public int id { get; set; }

        [Required(ErrorMessage = "RolId is required")]
        [ForeignKey(nameof(Rol))]
        public int rolId { get; set; }
        public Rol Rol { get; set; }

        [Required(ErrorMessage = "MenuId is required")]
        [ForeignKey(nameof(Menu))]
        public int menuId { get; set; }
        public Menu Menu { get; set; }
    }
}