using System;

public class UserDto
{
    public int userId { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public byte[] imgSrc { get; set; }
    public DateTime createdAt { get; set; }
    public bool archive { get; set; }
    public int rolId { get; set; }
}