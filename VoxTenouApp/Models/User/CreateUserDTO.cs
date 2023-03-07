using System.ComponentModel.DataAnnotations;

namespace VoxTenouApp.Models.User
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [StringLength(20, MinimumLength =8,ErrorMessage ="Password minimum 8 character")]
        public string password { get; set; }
        [Compare("password",ErrorMessage = "Password and Repeat password should match")]
        public string repeatPassword { get; set; }
    }
}
