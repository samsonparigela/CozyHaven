﻿using System.ComponentModel.DataAnnotations;

namespace CozyHaven.Models.DTOs
{
    public class RegisterUserDTO
    {
        //[Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Email is required")]
       // [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        public UserType UserType { get; set; }
    }
}
