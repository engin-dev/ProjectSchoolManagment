using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Students.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Students.API.Filters
{
    public class StudentParameterFilter : IParameterFilter
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public StudentParameterFilter(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (!parameter.Name.Equals("StudentName", StringComparison.InvariantCultureIgnoreCase)) return;
            using var scope = _serviceScopeFactory.CreateScope();
            var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var students = appContext.Students.ToArray();
            parameter.Schema.Enum = students.OrderBy(o=> o.FirstName).Select(p => new OpenApiString($"{p.FirstName}_{p.LastName}_{p.Identity}")).ToList<IOpenApiAny>();
        }
    }
}
