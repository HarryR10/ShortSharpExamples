using System;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationExample.Models
{
    public class Login
    {
        //Required - означает, что поле обязательно должно быть в теле запроса
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
