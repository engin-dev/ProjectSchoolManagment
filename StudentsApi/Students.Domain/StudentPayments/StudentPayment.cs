using System.ComponentModel.DataAnnotations.Schema;
using Students.Domain.Base;

namespace Students.Domain.StudentPayments
{
    [Table("StudentPayment")]
    public class StudentPayment : AuditEntity<int>
    {
        public StudentPayment()
        {
        }

        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        [ForeignKey(nameof(StudentId))] 
        public virtual Students.Student Student { get; set; }

        public bool ValidOnAdd()
        {
            return StudentId > 0 && Amount > 0;
        }
    }
}
