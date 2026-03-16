using Student.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Data.Interfaces
{
    public interface IStudenttRepository
    {
        Task<List<Studentt>> GetAllStudents();
        Task AddNewStudent(Studentt student);
        Task<Studentt> GetStudentByID(int id);
        Task DeleteStudent(Studentt student);
        Task UpdateStudent(Studentt student);
        Task<Studentt> GetStudentByEmail(string email);
    }
}
