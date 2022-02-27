using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Organic_Shop.ModelViews
{
    public class RegisterVM
    {
        [Key]
        public int CustomerId { get; set; }
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Please enter full name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter email")]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "ValidateEmail", controller: "Accounts")]
        public string Email { get; set; }
        [MaxLength(11)]
        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [Remote(action: "ValidatePhone", controller: "Accounts")]
        public string Phone { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter password")]
        [MinLength(5, ErrorMessage = "")]
        public string Password { get; set; }
        [MinLength(5, ErrorMessage = "The min length of password is 5")]
        [Display(Name = "Please re-enter the password")]
        [Compare("Password", ErrorMessage = "Please enter the same password")]
        public string ConfirmPassword { get; set; }
    }
}
