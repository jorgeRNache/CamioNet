using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using camionet.Models;
using camionet.Services;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;

namespace camionet.Controllers
{
    [ApiController]
    [Route("api/facturas")]
    public class FacturasController : ControllerBase
    {

        private readonly FirmaService _firmaService;
        private readonly HashService _hashService;
        private readonly CamioNetDbContext _context;

        private const string RutaCertificado = "certificado.pfx";
        private const string PassCertificado = "tu_password";


        public FacturasController(CamioNetDbContext context)
        {
            //_firmaService = new FirmaService();
            //_hashService = new HashService();
            _context = context;
        }


        //[Consumes("multipart/form-data")]
        //[HttpPost("enviar")]
        //public async Task<IActionResult> Enviar( [FromForm] IFormFile certificado, [FromForm] string password)
        //{

        //    if (certificado == null || certificado.Length == 0)
        //        return BadRequest("Debes subir un certificado.");

        //    using var stream = certificado.OpenReadStream();
        //    var cert = new X509Certificate2(ReadFully(stream), password);

        //    //aqui tengo que poner la generacion del verifactu

        //    return Ok($"Certificado recibido: {cert.Subject}");

        //}

        //private byte[] ReadFully(Stream input)
        //{
        //    using var ms = new MemoryStream();
        //    input.CopyTo(ms);
        //    return ms.ToArray();
        //}


        [HttpPost("CrearFactura")]
        public async Task<IActionResult> CrearFactura(/*[FromBody] FacturaVerifactu factura*/)
        {
            try
            {
                //var fileName = $"factura_{factura.Cabecera.Serie}_{factura.Cabecera.Numero}.xml";
                //var xml = SerializarFacturaXml(factura);

                //var ruta = Path.Combine("FacturasGeneradas", fileName);
                //Directory.CreateDirectory("FacturasGeneradas");
                //await System.IO.File.WriteAllTextAsync(ruta, xml);

                //return Ok(new
                //{
                //    mensaje = "Factura generada con éxito.",
                //    archivo = fileName,
                //    xml
                //});
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        //[HttpPost("EnviarFactura")]
        //public async Task<HttpResponseMessage> EnviarFacturaAEAT(string xmlFirmado, X509Certificate2 cert)
        //{
        //    var handler = new HttpClientHandler();
        //    handler.ClientCertificates.Add(cert);
        //    using var client = new HttpClient(handler);

        //    var content = new MultipartFormDataContent();
        //    var xmlContent = new StringContent(xmlFirmado, Encoding.UTF8, "application/xml");
        //    content.Add(xmlContent, "factura", "factura.xml");

        //    var response = await client.PostAsync("https://api.aeat.es/verifactu/envio", content);
        //    return response;
        //}


        //[HttpPost("FirmarFactura")]
        //public async Task<IActionResult> FirmarFactura([FromBody] FacturaVerifactu factura)
        //{
        //    try
        //    {
        //        // Cargar el certificado
        //        var cert = new X509Certificate2(RutaCertificado, PassCertificado);

        //        // Serializar la factura a XML
        //        var xml = SerializarFacturaXml(factura);

        //        // Firmar el XML
        //        var xmlFirmado = await _firmaService.FirmarXmlAsync(xml, cert);

        //        // Calcular el hash del XML firmado
        //        var hash = _hashService.CalcularHash(xmlFirmado);

        //        return Ok(new
        //        {
        //            mensaje = "Factura firmada con éxito.",
        //            xmlFirmado,
        //            hash
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}

        //[HttpGet("VerFacturas")]
        //[Authorize]
        //public async Task<IActionResult> VerFacturas()
        //{
        //    try
        //    {
        //        var fact = await _context.FacturaCabecera.ToListAsync();
               
                
        //        if (fact == null || fact.Count == 0)
        //        {
        //            return NotFound("No se encontraron facturas.");
        //        }

        //        return Ok(fact);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { error = ex.Message });
        //    }
        //}

        //private string SerializarFacturaXml(FacturaVerifactu factura)
        //{
        //    var ns = new XmlSerializerNamespaces();
        //    ns.Add(string.Empty, string.Empty); // eliminar xsi/xsd

        //    var serializer = new XmlSerializer(typeof(FacturaVerifactu));
        //    using var stringWriter = new StringWriter();
        //    serializer.Serialize(stringWriter, factura, ns);

        //    return stringWriter.ToString();
        //}
    }
}
