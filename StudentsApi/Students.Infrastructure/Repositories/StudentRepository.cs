using System;
using System.Collections.Generic;
using System.Linq;
using Students.Domain.Students;

namespace Students.Infrastructure.Repositories;

public class StudentRepository : Repository<Domain.Students.Student>, IStudentRepository
{
    public StudentRepository(DbFactory dbFactory) : base(dbFactory)
    {
    }

    public Student NewStudent(Student student)
    {
        if (student.ValidOnAdd())
        {
            this.Add(student);
            return student;
        }
        else
        {
            throw new Exception("Student invalid");
        }
    }

    public Student UpdateStudent(Student student)
    {
        if (student.ValidOnAdd())
        {
            this.Update(student);
            return student;
        }
        else
        {
            throw new Exception("Student invalid");
        }
    }

    public IQueryable<Student> GetStudents()
    {
        return this.GetAll();
    }

    public Student GetStudentByIdentity(string identity)
    {
        return this.Find(f => f.Identity.Equals(identity)).FirstOrDefault();
    }

    public IQueryable<Student> GetStudentsByStudentIds(List<int> ids)
    {
        return this.GetAll().Where(w => ids.Contains(w.Id));
    }
}