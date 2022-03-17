using AutoMapper;
using Students.Domain.Business;
using Students.Domain.Interfaces;
using Students.Domain.Schools;
using Students.Domain.Students;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.API.Services
{
    public class StudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository _studentRepository;
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;
        public StudentService(IUnitOfWork unitOfWork
            , IStudentRepository studentRepository
            , ISchoolRepository schoolRepository
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddStudentAsync(Student student, string schoolName)
        {
            var school = _schoolRepository.GetSchoolByName(schoolName);
            student.SchoolId = school.Id;
            _studentRepository.NewStudent(student);
            var saved = await _unitOfWork.CommitAsync();
            return saved > 0;
        }
        public async Task<bool> UpdateStudentAsync(Student student, string schoolName)
        {
            var existingStudent = _studentRepository.GetStudentByIdentity(student.Identity);
            if (existingStudent is null)
                return false;

            student.Id = existingStudent.Id;
            student.CreatedBy = existingStudent.CreatedBy;
            student.CreatedDate = existingStudent.CreatedDate;
            
            var school = _schoolRepository.GetSchoolByName(schoolName);
            student.SchoolId = school.Id;
            
            _studentRepository.UpdateStudent(student);
            var saved = await _unitOfWork.CommitAsync();
            return saved > 0;
        }

        public Task<List<StudentDto>> GetStudentsAsync()
        {
            var studentsResult = _studentRepository.GetStudents();

            return Task.FromResult(studentsResult.Select(s => new StudentDto
            {
                Identity = s.Identity,
                FirstName = s.FirstName,
                LastName = s.LastName,
                SchoolName=_schoolRepository.Get(s.SchoolId).Name,
                ClassName = s.ClassName,
                Email = s.Email,
                BirthDate = s.BirthDate,
                Gender = s.Gender
            }).ToList());
        }

        public Task<StudentDto> GetStudentsAsync(string identity)
        {
            var model = _studentRepository.GetStudentByIdentity(identity);
            if (model is not null)
            {
                return Task.FromResult(new StudentDto
                {
                    Identity = model.Identity,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    SchoolName = _schoolRepository.Get(model.SchoolId).Name,
                    ClassName = model.ClassName,
                    Email = model.Email,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender
                });
            }
            return Task.FromResult<StudentDto>(null);

        }
    }
}
