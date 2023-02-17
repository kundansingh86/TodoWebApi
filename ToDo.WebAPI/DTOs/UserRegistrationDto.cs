using System.ComponentModel.DataAnnotations;

namespace ToDo.WebAPI.DTOs
{
    public class UserRegistrationDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public IFormFile? Photo { get; set; }
    }
}
