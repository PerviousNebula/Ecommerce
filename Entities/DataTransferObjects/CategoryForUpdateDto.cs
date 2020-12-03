using System.ComponentModel.DataAnnotations;

public class CategoryForUpdateDto
{
    [Required(ErrorMessage = "CategoryId is required")]
    public int categoryId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
    public string name { get; set; }
            
    [Required(ErrorMessage = "Archive is required")]
    public bool archive { get; set; }
}
