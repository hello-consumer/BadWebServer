using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadWebServer.Models
{
    public class User
    {
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        [System.ComponentModel.DataAnnotations.Display(Name = "Your Email Address")]
        public string Username { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Your Password")]
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MinLength(8, ErrorMessage = "Your password must be at least 8 letters or numbers")]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^\w+$")] //Regular expression to only allow letters and numbers
        public string Password { get; set; }
    }
}
