//using BusinessLayer;
//using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyStudentApiProject.DTO_s;
using Student.Business.Interfaces;
using Student.Business.Services;
using System.Security.Claims;
using Student.Business;
using Student.Data.Entities;



namespace MyStudentApiProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        private readonly IstudenttService _Service;

        public StudentApiController(IstudenttService service)
        {
            _Service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetStudents", Name = "GetStudents")]
        public async Task<ActionResult> GetStudentsList()
        {
            var students = await _Service.GetAllStudentsAsync();

            var result = students.Select(s => new StudentDTO { ID = s.Id, Name = s.Name, Age = s.Age, Grade = s.Grade, Email = s.Email });

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddNewStudent", Name= "AddNewStudent")]
        public async Task<ActionResult> AddNewStudent(RegisterStudentDTO registerStudent)
        {
            var student = new Studentt
            {
                Name = registerStudent.Name,
                Age = registerStudent.Age,
                Grade = registerStudent.Grade,
                Email = registerStudent.Email,
                Role = registerStudent.Role,
                Password = registerStudent.Password
            };

            await _Service.AddNewStudent(student);

            var result = new StudentDTO { ID = student.Id,Name = student.Name, Age = student.Age, Grade = student.Grade, Email = student.Email };

            return CreatedAtAction(nameof(GetStudentsList),new {id = student.Id}, result);
        }

        [HttpGet("{id}",Name ="GetStudentByID")]
        public async Task<ActionResult<StudentDTO>> GetStudentByID(int id, [FromServices] IAuthorizationService authorizationService)
        {
            var authResult = await authorizationService.AuthorizeAsync(User, id, "StudentOwnerOrAdmin");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            Studentt student = await _Service.GetStudentByID(id);

            if (student == null)
                return NotFound("Student Not Found! ");

           

            StudentDTO SDTO = new StudentDTO
            {
                ID = student.Id,
                Name = student.Name,
                Age = student.Age,
                Grade = student.Grade,
                Email = student.Email
            };

            return Ok(SDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteStudent/{id}",Name = "DeleteStudent")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID! ");
            try
            {
                await _Service.DeleteStudent(id);

                return Ok("Student Deleted Successfully! ");
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }

            
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateStudent/{id}", Name = "UpdateStudent")]
        public async Task<ActionResult> UpdateStudent(int id, StudentDTO newstudent)
        {
            if (id <= 0)
                return BadRequest("Invalid ID! ");

            Studentt student = await _Service.GetStudentByID(id);

            if (student == null)
                return NotFound($"Student With ID : {id} Not Found! ");

            student.Name = newstudent.Name;
            student.Age = newstudent.Age;
            student.Grade = newstudent.Grade;
            student.Email = newstudent.Email;

            await _Service.UpdateStudent(student);

            return Ok("Student Updated! ");
        }

        [AllowAnonymous]
        [HttpGet("GetPassedStudents")]
        public async Task<ActionResult> GetPassedStudents()
        {
            List<Studentt> students = await _Service.GetPassedStudentsAsync();

            var result = students.Select(s => new StudentDTO { ID = s.Id, Name = s.Name, Age = s.Age, Grade = s.Grade, Email = s.Email }).ToList();

            return Ok(result);

        }

        [AllowAnonymous]
        [HttpGet("GetAverageGrade")]
        public async Task<ActionResult<double>> GetAverageGrade()
        {
            double average = await _Service.GetAverageGrade();

            return Ok(average);
        }



    }
}
