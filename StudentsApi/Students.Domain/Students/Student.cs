using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Students.Domain.Base;
using Students.Domain.Schools;
using Students.Domain.StudentPayments;

namespace Students.Domain.Students;

[Table("Student")]
public class Student : AuditEntity<int>
{
    public Student()
    {
        StudentPayment = new HashSet<StudentPayment>();
    }
    public int SchoolId { get; set; }
    public string Identity { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ClassName { get; set; }
    public DateTime BirthDate { get; set; }

    [EmailAddress] 
    public string Email { get; set; }
    public char Gender { get; set; }

    [ForeignKey(nameof(SchoolId))]
    public virtual School School { get; set; }
    public ICollection<StudentPayment> StudentPayment { get; set; }

    public bool ValidOnAdd()
    {
        return
            !string.IsNullOrEmpty(Identity)
            && !string.IsNullOrEmpty(FirstName)
            && !string.IsNullOrEmpty(LastName)
            && !string.IsNullOrEmpty(Email)
            && new EmailAddressAttribute().IsValid(Email);
    }
}