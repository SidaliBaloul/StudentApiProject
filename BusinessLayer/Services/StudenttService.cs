using Student.Business.Interfaces;
using Student.Data;
using Student.Data.Entities;
using Student.Data.Interfaces;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Business.Services
{
    public class StudenttService : IstudenttService
    {
        private readonly IStudenttRepository _Repository;

        public StudenttService(IStudenttRepository repository)
        {
            _Repository = repository;
        }

        public async Task<List<Studentt>> GetAllStudentsAsync()
        {
            return await _Repository.GetAllStudents();
        }

        public async Task AddNewStudent(Studentt student)
        {
           

            student.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);

            await _Repository.AddNewStudent(student);
        }

        public async Task<Studentt> GetStudentByID(int id)
        {
            return await _Repository.GetStudentByID(id);
        }

        public async Task DeleteStudent(int id)
        {
            Studentt student = await _Repository.GetStudentByID(id);

            if (student == null)
                throw new KeyNotFoundException($"Student With ID : {id} Not Found! ");

            await _Repository.DeleteStudent(student);
        }

        public async Task UpdateStudent(Studentt student)
        {
            await _Repository.UpdateStudent(student);
        }

        public async Task<List<Studentt>> GetPassedStudentsAsync()
        {
            List<Studentt> students = await _Repository.GetAllStudents();

            return students.Where(s => s.Grade >= 50).ToList();
        }

        public async Task<double> GetAverageGrade()
        {
            List<Studentt> students = await _Repository.GetAllStudents();

            return students.Average(s => s.Grade);
        }

        public async Task<Studentt> GetStudentByEmail(string email)
        {
            return await _Repository.GetStudentByEmail(email);
        }
    }
}
