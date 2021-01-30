using System.Collections.Generic;

public class MenuDto
{
    public int menuId { get; set; }
    public string title { get; set; }
    public string icon { get; set; }
    public string url { get; set; }
    public ICollection<MenuItemDto> MenuItems { get; set; }
}