using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Students.API.Services;
using Students.Domain.Business;
using Students.Domain.Schools;
using System.Threading.Tasks;

namespace Students.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly SchoolService _service;
        private readonly IMapper _mapper;

        public SchoolsController(SchoolService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("AddSchool")]
        public async Task<IActionResult> Post([FromQuery] SchoolDto school)
        {
            var model = _mapper.Map<School>(school);

            var result = await _service.AddSchoolAsync(model);

            return Ok(result);
        }

        [HttpGet("GetSchools")]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetSchoolsAsync();

            return Ok(result);
        }

       
    }
}
