﻿using System.ComponentModel.DataAnnotations;

namespace Xero.Products.App.Authentication.Models;
public class UserModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}
