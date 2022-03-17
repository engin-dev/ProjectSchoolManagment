using Students.Domain.Business;
using Students.Domain.Interfaces;
using Students.Domain.Schools;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.API.Services
{
    public class SchoolService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISchoolRepository _schoolRepository;


        public SchoolService(IUnitOfWork unitOfWork
            , ISchoolRepository schoolRepository)
        {
            _unitOfWork = unitOfWork;
            _schoolRepository = schoolRepository;
        }

        
        public async Task<bool> AddSchoolWithNameAsync(string schoolName)
        {
            _schoolRepository.AddSchoolWithName(schoolName);
            var saved = await _unitOfWork.CommitAsync();
            return saved > 0;
        }

        public async Task<bool> AddSchoolAsync(School school)
        {
            _schoolRepository.AddSchool(school);
            var saved = await _unitOfWork.CommitAsync();
            return saved > 0;
        }
        
        public Task<List<SchoolDto>> GetSchoolsAsync()
        {
            var schoolsResult = _schoolRepository.GetSchools();
            
            return Task.FromResult(schoolsResult.Select(s => new SchoolDto
            {
                Name = s.Name
            }).ToList());
        }
    }
}
