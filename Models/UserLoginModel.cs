﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace last_try_api.Models
{
    [NotMapped]
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }

}
