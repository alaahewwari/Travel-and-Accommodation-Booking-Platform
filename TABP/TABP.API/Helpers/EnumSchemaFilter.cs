using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TABP.API.Helpers
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var enumNames = Enum.GetNames(context.Type);
                var enumValues = Enum.GetValues(context.Type).Cast<byte>();

                var enumDescriptions = enumValues
                    .Zip(enumNames, (value, name) => $"{value} = {name}");

                schema.Description += " Possible values: " + string.Join(", ", enumDescriptions);
            }
        }
    }
}
