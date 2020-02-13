using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.DTO
{
    public class UserForRegisterDTO
    {
        [Required(ErrorMessage ="UserName is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Min Password length should be 8")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(8, MinimumLength =4, ErrorMessage ="Min Password length should be 4")]
        public string Password { get; set; }
    }
}
