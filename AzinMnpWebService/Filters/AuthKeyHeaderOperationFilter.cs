using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace AzinMnpWebService.Filters;

public class AuthKeyHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAuthKeyAttribute = context.MethodInfo
                                      .DeclaringType?.GetCustomAttributes(true)
                                      .OfType<AuthKeyRequiredAttribute>()
                                      .Any() == true
                                  ||
                                  context.MethodInfo
                                      .GetCustomAttributes(true)
                                      .OfType<AuthKeyRequiredAttribute>()
                                      .Any();

        if (hasAuthKeyAttribute)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Consumer-Key",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                },
                Description = "Authorization key required for MNP operations"
            });
        }
    }
}