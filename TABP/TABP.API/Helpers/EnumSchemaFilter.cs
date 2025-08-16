using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TABP.API.Helpers
{
    /// <summary>
    /// Custom schema filter for enhancing enum documentation in Swagger/OpenAPI specifications.
    /// Automatically adds detailed descriptions showing all possible enum values and their names.
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies custom formatting to enum schemas by adding detailed value descriptions.
        /// Enhances enum documentation by listing all possible values with their numeric values and names.
        /// </summary>
        /// <param name="schema">The OpenAPI schema being processed for the enum type.</param>
        /// <param name="context">The schema filter context containing type information and metadata.</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                var enumNames = Enum.GetNames(context.Type);
                var enumValues = Enum.GetValues(context.Type).Cast<int>();
                var enumDescriptions = enumValues
                    .Zip(enumNames, (value, name) => $"{value} = {name}");
                schema.Description += " Possible values: " + string.Join(", ", enumDescriptions);
            }
        }
    }
}