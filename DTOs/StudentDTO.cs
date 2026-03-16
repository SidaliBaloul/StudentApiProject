using System.ComponentModel.DataAnnotations;

namespace MyStudentApiProject.DTO_s
{
    public class StudentDTO
    {

        public int ID { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age Is Required"), Range(18, 40, ErrorMessage = "Age Must Be Between 18 And 40! ")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Grade Is Required"), Range(0, 100, ErrorMessage = "Grade Must Be Between 0 And 100")]
        public int Grade { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }

    }
}
