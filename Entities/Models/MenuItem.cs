using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("MenuItem")]
    public class MenuItem : IEntity
    {
        [Key]
        [Column("menuItemId")]
        public int id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(45, ErrorMessage = "Title can't be longer than 45 characters")]
        public string title { get; set; }

        [StringLength(45, ErrorMessage = "Icon can't be longer than 45 characters")]
        public string icon { get; set; }

        [Required(ErrorMessage = "Url is required")]
        [StringLength(45, ErrorMessage = "Url can't be longer than 45 characters")]
        public string url { get; set; }

        [Required(ErrorMessage = "menuId is required")]
        [ForeignKey(nameof(Menu))]
        public int menuId { get; set; }
        public Menu Menu { get; set; }
    }
}