using Microsoft.OpenApi.Models;
using Org.BouncyCastle.Tls;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace camionet.Services.SwaggerInputsFiltros
{
    public class SwaggerFiltroPasswordParametro : SwaggerFiltroBase, IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            var parametrosString = context.MethodInfo.GetParameters().Where(a => a.ParameterType == typeof(string)).ToList();

            if (parametrosString.Count == 0)
                return;

            var formParameters = context.MethodInfo.GetParameters().ToList();

            if (!formParameters.Any())
                return;

            var propiedades = new Dictionary<string, OpenApiSchema>();

            foreach (var parametro in formParameters)
            {
                propiedades[parametro.Name] = CatalogoParametros(parametro);
            }

            AddParametrosBody(operation, propiedades);

        }


        public static bool CheckPassword(ParameterInfo parametro)
        {
            if (parametro.ParameterType == typeof(string))
            {
                if (parametro.Name.ToLower().Contains("password"))
                {
                    return true;
                }
            }

            return false;
        }

        public static OpenApiSchema GeneratePassword(ParameterInfo parametro)
        {
            if (CheckPassword(parametro))
            {
                return new OpenApiSchema
                {
                    Type = "string",
                    Format = "password"
                };
            }

            return GenerateParametroNormal(parametro);

        }
    }
}
