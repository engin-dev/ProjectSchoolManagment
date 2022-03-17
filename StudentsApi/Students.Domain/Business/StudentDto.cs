using System;

namespace Students.Domain.Business
{
    public class StudentDto
    {
        public string Identity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClassName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public char Gender { get; set; }

        public string SchoolName { get; set; }
    }
}
