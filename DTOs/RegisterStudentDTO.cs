using System.ComponentModel.DataAnnotations;

namespace MyStudentApiProject.DTO_s
{
    public class RegisterStudentDTO
    {
        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age Is Required"), Range(18, 40,ErrorMessage = "Age Must Be Between 18 And 40! ")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Grade Is Required"), Range(0, 100,ErrorMessage = "Grade Must Be Between 0 And 100")]
        public int Grade { get; set; }

        [Required(ErrorMessage = "Role Is Required")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required."),Length(4,20, ErrorMessage = "Password Must Be Atleast 4 Characters And 20 Max.")]
        public string Password { get; set; }

    }
}
