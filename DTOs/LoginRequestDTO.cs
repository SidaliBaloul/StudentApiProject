using System.ComponentModel.DataAnnotations;

namespace MyStudentApiProject.DTO_s
{
    public class LoginRequestDTO
    {

        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required."), Length(4, 20, ErrorMessage = "Password Must Be Atleast 4 Characters And 20 Max.")]
        public string Password { get; set; }
    }
}
