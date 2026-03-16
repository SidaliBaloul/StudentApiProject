using Student.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Business.Interfaces
{
    public interface IstudenttService
    {
        Task<List<Studentt>> GetAllStudentsAsync();
        Task AddNewStudent(Studentt student);
        Task<Studentt> GetStudentByID(int id);
        Task DeleteStudent(int id);
        Task UpdateStudent(Studentt student);
        Task<List<Studentt>> GetPassedStudentsAsync();
        Task<double> GetAverageGrade();
        Task<Studentt> GetStudentByEmail(string  email);
    }
}
