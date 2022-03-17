using Students.Domain.Schools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Students.Infrastructure.Repositories
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public SchoolRepository(DbFactory dbFactory) : base(dbFactory)
        {
        }

        public School NewSchool(string name)
        {
            var school = new School
            {
                Name = name
            };
            if (school.ValidOnAdd())
            {
                this.Add(school);
                return school;
            }
            else
            {
                throw new Exception("School invalid");
            }
        }

        public School AddSchool(School school)
        {
            if (school.ValidOnAdd())
            {
                this.Add(school);
                return school;
            }
            else
            {
                throw new Exception("School invalid");
            }
        }

        public School AddSchoolWithName(string name)
        {
            var school = new School
            {
                Name = name
            };
            if (school.ValidOnAdd())
            {
                this.Add(school);
                return school;
            }
            else
            {
                throw new Exception("School invalid");
            }
        }

        public IQueryable<School> GetSchools() 
        {
            return this.GetAll();
        }

        public School GetSchoolByName(string name)
        {
            return this.Find(f => f.Name == name).FirstOrDefault();
        }
    }
}
