using FirmaXadesNetCore;
using FirmaXadesNetCore.Crypto;
using FirmaXadesNetCore.Signature.Parameters;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;

namespace camionet.Services
{
    public class FirmaService
    {
        public string FirmarXmlConXades(string xmlSinFirmar, string rutaCertificado, string passCert)
        {
            var cert = new X509Certificate2(rutaCertificado, passCert, X509KeyStorageFlags.Exportable);

            var xmlBytes = Encoding.UTF8.GetBytes(xmlSinFirmar);
            using var stream = new MemoryStream(xmlBytes);

            var parameters = new SignatureParameters
            {
                SignatureMethod = SignatureMethod.RSAwithSHA256,
                DigestMethod = DigestMethod.SHA256,
                SignaturePackaging = SignaturePackaging.ENVELOPED,
                Signer = new Signer(cert),
                DataFormat = new DataFormat
                {
                    MimeType = "text/xml"
                }
            };

            var xadesService = new XadesService();

            var signedDoc = xadesService.Sign(stream, parameters);

            // Firmado en string
            string xmlFirmado = signedDoc.ToString();

            return xmlFirmado;
        }
    }
}
