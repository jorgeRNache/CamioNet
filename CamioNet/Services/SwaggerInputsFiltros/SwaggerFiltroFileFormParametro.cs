namespace camionet.Services.SwaggerInputsFiltros
{
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Reflection;

    //clase para que en el swagger se vea el selector de archivo como un selector de archivo normal y corriente
    public class SwaggerFiltroFileFormParametro : SwaggerFiltroBase, IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //comprobamos si existe algun parametro que se llame certificado y que sea de tipo IFromFile
            var parametrosIformFileCertificado = context.MethodInfo.GetParameters().Where(a => a.ParameterType == typeof(IFormFile)).Where(a => a.Name == "certificado").ToList();

            if (parametrosIformFileCertificado.Count == 0)
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


        public static OpenApiSchema GenerateSelectorArchivos(ParameterInfo parametro)
        {
            if(parametro.ParameterType == typeof(IFormFile))
            {
                return new OpenApiSchema
                {
                    Type = "string",
                    Format = "binary"
                };
            }

            return GenerateParametroNormal(parametro);

        }

    }
}
