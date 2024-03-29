﻿using System.ComponentModel.DataAnnotations;

namespace Registration.Api.Dtos.User;

/// <summary>
/// Update user request.
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Username.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Username { get; set; }

    /// <summary>
    /// Password.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Password { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    [Required]
    [StringLength(50)]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
    public string Email { get; set; }

    /// <summary>
    /// Company name.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string CompanyName { get; set; }
}
