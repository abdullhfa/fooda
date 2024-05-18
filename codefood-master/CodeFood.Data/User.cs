﻿namespace CodeFood.Data;

public class User : BaseEntity
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; } = "user";
}
