﻿namespace Registration.Services.DTOs.Users;
public class GetUserResponse
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public long CompanyId { get; set; }
}
