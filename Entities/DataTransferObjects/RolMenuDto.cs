public class RolMenuDto
{
    public int rolMenuId { get; set; }
    public int rolId { get; set; }
    public int menuId { get; set; }
    public RolDto Rol { get; set; }
    public MenuDto Menu { get; set; }
}