using System.ComponentModel.DataAnnotations;

namespace MyStudentApiProject.DTO_s;

public class LogoutRequest
{
    [Required(ErrorMessage = "Email Is Required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Refresh Token Is Required")]
    public string RefreshToken { get; set; }
}
