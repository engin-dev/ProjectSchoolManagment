using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Students.API.Services;
using Students.Domain.Business;
using Students.Domain.Students;
using System.Threading.Tasks;

namespace Students.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly StudentService _service;

    private readonly IMapper _mapper;

    public StudentsController(StudentService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost("AddStudents")]
    public async Task<IActionResult> Post([FromQuery] StudentDto student)
    {
        var model = _mapper.Map<Student>(student);
        
        var result = await _service.AddStudentAsync(model, student.SchoolName);

        return Ok(result);
    }

    [HttpPut("UpdateStudents")]
    public async Task<IActionResult> Put([FromQuery] StudentDto student)
    {
        var model = _mapper.Map<Student>(student);

        var result = await _service.UpdateStudentAsync(model, student.SchoolName);

        return Ok(result);
    }

    [HttpGet("GetAllStudents")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetStudentsAsync();

        return Ok(result);
    }

    [HttpGet("GetStudents")]
    public async Task<IActionResult> GetById(string identity)
    {
        var result = await _service.GetStudentsAsync(identity);

        return Ok(result);
    }
}