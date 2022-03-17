using System;
using Microsoft.AspNetCore.Mvc;
using Students.API.Services;
using System.Threading.Tasks;
using AutoMapper;
using Students.Domain.Business;
using Students.Domain.StudentPayments;

namespace Students.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentPaymentController : ControllerBase
    {
        private readonly StudentPaymentService _service;
        private readonly IMapper _mapper;

        public StudentPaymentController(StudentPaymentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        [HttpPost]
        [Route("AddStudentPayment")]
        public async Task<IActionResult> Post([FromQuery] StudentPaymentDto studentPayment)
        {
            var model = _mapper.Map<StudentPayment>(studentPayment);

            var result = await _service.AddStudentPaymentAsync(model, studentPayment.StudentName);

            return Ok(result);
        }

        [HttpGet("GetAllStudentsPayment")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetStudentsPaymentAsync();

            return Ok(result);
        }

        [HttpGet("GetStudentsPayment")]
        public async Task<IActionResult> GetById(string studentName)
        {
            var result = await _service.GetStudentsPaymentAsync(studentName);

            return Ok(result);
        }

        [HttpGet("GetStudentPaymentsByDate")]
        public async Task<IActionResult> GetStudentPaymentsByDate(DateTime startDate, DateTime endDate)
        {
            var result = await _service.GetStudentPaymentsByDateAsync(startDate, endDate);

            return Ok(result);
        }
    }
}
