using System.ComponentModel.DataAnnotations;

namespace VoxTenouApp.Models.User
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password minimum 8 character")]
        public string Password { get; set; }
    }
}
