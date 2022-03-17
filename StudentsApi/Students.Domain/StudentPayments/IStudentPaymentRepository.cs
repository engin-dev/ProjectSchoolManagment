using System;
using System.Linq;
using Students.Domain.Interfaces;

namespace Students.Domain.StudentPayments
{
    public interface IStudentPaymentRepository : IRepository<StudentPayment>
    {
        StudentPayment NewStudentPayment(StudentPayment studentPayment);
        IQueryable<StudentPayment> GetStudentPayments();
        StudentPayment GetStudentPaymentById(int studentId);
        IQueryable<StudentPayment> GetStudentByDate(DateTime startDate, DateTime endDate);
    }
}
