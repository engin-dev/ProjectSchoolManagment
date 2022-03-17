using System;
using System.Collections.Generic;
using System.Linq;
using Students.Domain.Interfaces;

namespace Students.Domain.Students;

public interface IStudentRepository : IRepository<Domain.Students.Student>
{
    Student NewStudent(Student student);
    Student UpdateStudent(Student student);
    Student GetStudentByIdentity(string identity);
    IQueryable<Student> GetStudents();
    IQueryable<Student> GetStudentsByStudentIds(List<int> ids);
}