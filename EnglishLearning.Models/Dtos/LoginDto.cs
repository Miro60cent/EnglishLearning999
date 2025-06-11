using System.ComponentModel.DataAnnotations;

namespace Topic.Models.Dtos
{
    public class LoginDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Слишком длинная строка")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}