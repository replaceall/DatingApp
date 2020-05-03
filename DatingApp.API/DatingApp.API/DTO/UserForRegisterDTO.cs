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
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Min Password length should be 4")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength =4, ErrorMessage ="Min Password length should be 4")]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDTO()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}
