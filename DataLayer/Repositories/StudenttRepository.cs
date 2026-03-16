using Microsoft.EntityFrameworkCore;
using Student.Data.DBContext;
using Student.Data.Entities;
using Student.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Data.Repositories
{
    public class StudenttRepository : IStudenttRepository
    {
        private readonly AppDBContext _Context;

        public StudenttRepository(AppDBContext context)
        {
            _Context = context;
        }
        
        public async Task<List<Studentt>> GetAllStudents()
        {
            return await _Context.Students.ToListAsync();
        }

        public async Task AddNewStudent(Studentt student)
        {
              _Context.Students.Add(student);
            await _Context.SaveChangesAsync();
        }

        public async Task<Studentt> GetStudentByID(int id)
        {
            return await _Context.Students.FirstOrDefaultAsync(student => student.Id == id);
        }

        public async Task DeleteStudent(Studentt student)
        {
            _Context.Students.Remove(student);

            await _Context.SaveChangesAsync();
        }

        public async Task UpdateStudent(Studentt student)
        {
            _Context.Students.Update(student);
            await _Context.SaveChangesAsync();
        }

        public async Task<Studentt> GetStudentByEmail(string email)
        {
            return await _Context.Students.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
