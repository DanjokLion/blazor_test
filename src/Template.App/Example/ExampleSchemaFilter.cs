using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using Template.Domain.Dto;

namespace Template.App.Example
{
    internal class ExampleSchemaFilter : ISchemaFilter
    {
        private readonly JsonSerializerSettings _settings = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter> { new Newtonsoft.Json.Converters.StringEnumConverter() }
        };

        private static readonly ProblemDetails ProblemDetailsObject = new()
        {
            Type = "Microsoft.AspNetCore.Http.BadHttpRequestException",
            Title = "One or more validation errors occurred",
            Status = 400
        };

        private static readonly TemplateDto TemplateDtoObject = new()
        { Email = "dsadasd"
        };

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            object example = context.Type.Name switch
            {
                nameof(ProblemDetails) => ProblemDetailsObject,
                nameof(TemplateDto) => TemplateDtoObject,
                _ => null
            };

            if (example is not null)
                schema.Example = new OpenApiString(JsonConvert.SerializeObject(example, _settings));
        }
    }
}