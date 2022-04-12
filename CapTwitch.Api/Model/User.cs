﻿using System.ComponentModel.DataAnnotations;

namespace CapTwitch.Api.Model;

public class User : IStoredObject
{
    public int Id { get; set; }
    [Required]
    public string Pseudo { get; set; }
    public string PasswordHash { get; set; }
}

public class SignIn
{
    public string Pseudo { get; set; }
    public string Password { get; set; }
}