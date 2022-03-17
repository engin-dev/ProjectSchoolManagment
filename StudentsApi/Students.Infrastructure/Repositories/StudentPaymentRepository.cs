using System;
using System.Linq;
using Students.Domain.StudentPayments;

namespace Students.Infrastructure.Repositories
{
    public class StudentPaymentRepository : Repository<StudentPayment>, IStudentPaymentRepository
    {
        public StudentPaymentRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public StudentPayment NewStudentPayment(StudentPayment studentPayment)
        {
            if (studentPayment.ValidOnAdd())
            {
                this.Add(studentPayment);
                return studentPayment;
            }
            else
                throw new Exception("Student Payment invalid");
        }

        public IQueryable<StudentPayment> GetStudentPayments()
        {
            return this.GetAll();
        }

        public StudentPayment GetStudentPaymentById(int studentId)
        {
            return this.Find(f => f.StudentId.Equals(studentId)).FirstOrDefault();
        }

        public IQueryable<StudentPayment> GetStudentByDate(DateTime startDate, DateTime endDate)
        {
            return this.Find(f => f.CreatedDate.Date >= startDate.Date && f.CreatedDate.Date <= endDate.Date);
        }
    }
}
