using Students.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Students.Domain.Schools
{
    [Table("School")]
    public class School : AuditEntity<int>
    {
        public School()
        {
            
        }
        public string Name { get; set; }
        public bool ValidOnAdd()
        {
            return
                !string.IsNullOrEmpty(Name);
        }

    }
}
