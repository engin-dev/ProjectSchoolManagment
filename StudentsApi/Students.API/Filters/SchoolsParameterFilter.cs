using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Students.Infrastructure;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;

namespace Students.API.Filters
{
    public class SchoolsParameterFilter : IParameterFilter
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SchoolsParameterFilter(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (!parameter.Name.Equals("SchoolName", StringComparison.InvariantCultureIgnoreCase)) return;
            using var scope = _serviceScopeFactory.CreateScope();
            var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var schools = appContext.Schols.ToArray();
            parameter.Schema.Enum = schools.Select(p => new OpenApiString(p.Name)).ToList<IOpenApiAny>();
        }
    }
}
