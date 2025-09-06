using Microsoft.OpenApi.Models;
using System.Reflection;

namespace camionet.Services.SwaggerInputsFiltros
{
    public class SwaggerFiltroBase
    {
        public static void AddParametrosBody(OpenApiOperation operation, Dictionary<string, OpenApiSchema> propiedades)
        {
            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = propiedades,
                        Required = propiedades.Keys.ToHashSet()
                    }
                }
            }
            };
        }


        public static OpenApiSchema GenerateParametroNormal(ParameterInfo parametro)
        {
            return new OpenApiSchema
            {
                Type = parametro.ParameterType.Name.ToLower()
            };
        }

        public static OpenApiSchema CatalogoParametros(ParameterInfo parametro)
        {
            var type = parametro.ParameterType;

            if (type == typeof(IFormFile))
            {
                return SwaggerFiltroFileFormParametro.GenerateSelectorArchivos(parametro);
            }
            else if(type == typeof(string))
            {
                return SwaggerFiltroPasswordParametro.GeneratePassword(parametro);
            }


            return GenerateParametroNormal(parametro);
        }

    }
}
