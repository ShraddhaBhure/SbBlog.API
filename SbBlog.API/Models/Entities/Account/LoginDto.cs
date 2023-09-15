using System.ComponentModel.DataAnnotations;

namespace SbBlog.API.Models.Entities.Account
{
    public class LoginDto
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
       

    }
}
