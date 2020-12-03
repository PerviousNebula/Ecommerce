using System.ComponentModel.DataAnnotations;

public class CategoryCreationDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(45, ErrorMessage = "Name can't be longer than 45 characters")]
    public string name { get; set; }
            
    [Required(ErrorMessage = "Archive is required")]
    public bool archive { get; set; }
}
