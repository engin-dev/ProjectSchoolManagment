using AutoMapper;
using Students.Domain.Business;
using Students.Domain.Interfaces;
using Students.Domain.StudentPayments;
using Students.Domain.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.API.Services
{
    public class StudentPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentPaymentRepository _studentPaymentRepository;
        private readonly IMapper _mapper;
        public StudentPaymentService(IUnitOfWork unitOfWork
            , IStudentRepository studentRepository
            , IStudentPaymentRepository studentPaymentRepository
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
            _studentPaymentRepository = studentPaymentRepository;
            _mapper = mapper;
        }
        public async Task<bool> AddStudentPaymentAsync(StudentPayment studentPayment, string studentName)
        {
            if (string.IsNullOrEmpty(studentName)) return false;

            var identity = string.Empty;
            var studentNameArray = studentName.Split("_");
            if (studentNameArray.Any() && studentNameArray.Length == 3)
            {
                identity = studentNameArray[2];
            }

            if (string.IsNullOrEmpty(identity)) return false;

            var student = _studentRepository.GetStudentByIdentity(identity);

            studentPayment.StudentId = student.Id;

            _studentPaymentRepository.NewStudentPayment(studentPayment);
            var saved = await _unitOfWork.CommitAsync();
            return saved > 0;
        }

        public Task<List<StudentPaymentDto>> GetStudentsPaymentAsync()
        {
            var studentsPaymentResult = _studentPaymentRepository.GetStudentPayments();

            return Task.FromResult(studentsPaymentResult.Select(s => new StudentPaymentDto
            {
                StudentName = _studentRepository.Get(s.StudentId).FirstName,
                Description = s.Description,
                Amount = s.Amount
            }).ToList());
        }
        public Task<StudentPaymentDto> GetStudentsPaymentAsync(string studentName)
        {
            var identity = string.Empty;
            var studentNameArray = studentName.Split("_");
            if (studentNameArray.Any() && studentNameArray.Length == 3)
            {
                identity = studentNameArray[2];
            }
            var student = _studentRepository.GetStudentByIdentity(identity);
            var studentsPaymentResult = _studentPaymentRepository.GetStudentPaymentById(student.Id);
            if (studentsPaymentResult is not null)
            {
                var model = _mapper.Map<StudentPayment>(studentsPaymentResult);
                return Task.FromResult(new StudentPaymentDto
                {
                    StudentName = _studentRepository.Get(model.StudentId).FirstName,
                    Description = model.Description,
                    Amount = model.Amount
                });
            }
            return Task.FromResult<StudentPaymentDto>(null);
        }

        public Task<List<StudentPaymentDto>> GetStudentPaymentsByDateAsync(DateTime startDate, DateTime endDate)
        {
            
            var studentPayments = _studentPaymentRepository.GetStudentByDate(startDate, endDate).ToList();
            if (studentPayments is not null)
            {
                var students = _studentRepository.GetStudentsByStudentIds(studentPayments.Select(s => s.StudentId).ToList()).ToList();

                var result = from sp in studentPayments
                             join p in students
                                on sp.StudentId equals p.Id
                             select new StudentPaymentDto
                             {
                                 StudentName = $"{p.FirstName} {p.LastName}" ,
                                 Amount = sp.Amount,
                                 Description = sp.Description
                             };
                
                return Task.FromResult<List<StudentPaymentDto>>(result.ToList());
            }
            return Task.FromResult<List<StudentPaymentDto>>(null);
        }
    }

}
