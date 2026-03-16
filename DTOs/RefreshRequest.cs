using System.ComponentModel.DataAnnotations;

namespace MyStudentApiProject.DTO_s
{
    public class RefreshRequest
    {
        [Required(ErrorMessage = "Refresh Token Is Required")]
        public string RefreshToken { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }
    }
}
