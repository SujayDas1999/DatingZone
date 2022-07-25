using System.ComponentModel.DataAnnotations;

namespace DatingZone.Entities.Dtos
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
