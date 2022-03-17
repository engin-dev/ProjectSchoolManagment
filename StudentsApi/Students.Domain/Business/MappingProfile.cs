using AutoMapper;
using Students.Domain.Schools;
using Students.Domain.StudentPayments;
using Students.Domain.Students;

namespace Students.Domain.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SchoolDto, School>();
            CreateMap<StudentDto, Student>();
            CreateMap<StudentPaymentDto, StudentPayment>();
        }
    }
}
