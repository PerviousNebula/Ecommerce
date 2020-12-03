using System.Collections.Generic;

public class RolDto
{
    public int rolId { get; set; }
    public string name { get; set; }
    public ICollection<UserDto> Users { get; set; }
}