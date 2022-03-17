using Students.Domain.Interfaces;
using System.Linq;

namespace Students.Domain.Schools
{
    public interface ISchoolRepository : IRepository<School>
    {
        School AddSchool(School school);
        School AddSchoolWithName(string name);
        IQueryable<School> GetSchools();
        School GetSchoolByName(string name);
    }
}
