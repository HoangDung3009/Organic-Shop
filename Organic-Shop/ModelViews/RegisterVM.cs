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
        [MaxLength(100)]
        [Required(ErrorMessage = "Please enter email !!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter password !!")]
        //  [MinLength(5, ErrorMessage = "Min length is 5")]
        public string Password { get; set; }
    }
}
