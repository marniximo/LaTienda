using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LaTienda.Clientes.AFIP
{
    public class LoginTicket
    {
        public UInt32 UniqueId; // Entero de 32 bits sin signo que identifica el requerimiento
        public DateTime GenerationTime; // Momento en que fue generado el requerimiento
        public DateTime ExpirationTime; // Momento en el que expira la solicitud
        public string Service; // Identificacion del WSN para el cual se solicita el TA
        public string Sign; // Firma de seguridad recibida en la respuesta
        public string Token; // Token de seguridad recibido en la respuesta
        public XmlDocument XmlLoginTicketRequest = null;
        public XmlDocument XmlLoginTicketResponse = null;
        public string XmlStrLoginTicketRequestTemplate = "<loginTicketRequest><header><uniqueId></uniqueId><generationTime></generationTime><expirationTime></expirationTime></header><service></service></loginTicketRequest>";
        private static UInt32 _globalUniqueID = 0; // OJO! NO ES THREAD-SAFE

        public async Task<string> ObtenerLoginTicketResponse()
        {
            string cmsFirmadoBase64 = null;
            string loginTicketResponse = null;
            XmlNode xmlNodoUniqueId = default(XmlNode);
            XmlNode xmlNodoGenerationTime = default(XmlNode);
            XmlNode xmlNodoExpirationTime = default(XmlNode);
            XmlNode xmlNodoService = default(XmlNode);

            // PASO 1: Genero el Login Ticket Request
            try
            {
                _globalUniqueID += 1;

                XmlLoginTicketRequest = new XmlDocument();
                XmlLoginTicketRequest.LoadXml(XmlStrLoginTicketRequestTemplate);

                xmlNodoUniqueId = XmlLoginTicketRequest.SelectSingleNode("//uniqueId");
                xmlNodoGenerationTime = XmlLoginTicketRequest.SelectSingleNode("//generationTime");
                xmlNodoExpirationTime = XmlLoginTicketRequest.SelectSingleNode("//expirationTime");
                xmlNodoService = XmlLoginTicketRequest.SelectSingleNode("//service");
                xmlNodoGenerationTime.InnerText = DateTime.Now.AddMinutes(-10).ToString("s");
                xmlNodoExpirationTime.InnerText = DateTime.Now.AddMinutes(+10).ToString("s");
                xmlNodoUniqueId.InnerText = Convert.ToString(_globalUniqueID);
                xmlNodoService.InnerText = "wsfe";
            }
            catch (Exception excepcionAlGenerarLoginTicketRequest)
            {
                throw new Exception("Error GENERANDO el LoginTicketRequest : " + excepcionAlGenerarLoginTicketRequest.Message + excepcionAlGenerarLoginTicketRequest.StackTrace);
            }

            // PASO 2: Firmo el Login Ticket Request
            try
            {
                X509Certificate2 certFirmante = Certificado.ObtenerCertificadoDesdeArchivo();

                // Convierto el Login Ticket Request a bytes, firmo el msg y lo convierto a Base64
                Encoding EncodedMsg = Encoding.UTF8;
                byte[] msgBytes = EncodedMsg.GetBytes(XmlLoginTicketRequest.OuterXml);
                byte[] encodedSignedCms = Certificado.FirmaBytesMensaje(msgBytes, certFirmante);
                cmsFirmadoBase64 = Convert.ToBase64String(encodedSignedCms);
            }
            catch (Exception excepcionAlFirmar)
            {
                throw new Exception("Error FIRMANDO el LoginTicketRequest : " + excepcionAlFirmar.Message);
            }

            // PASO 3: Invoco al WSAA para obtener el Login Ticket Response
            try
            {
                var clienteWsaa = new Wsaa.LoginCMSClient();

                loginTicketResponse = (await clienteWsaa.loginCmsAsync(cmsFirmadoBase64)).loginCmsReturn;

            }
            catch (Exception excepcionAlInvocarWsaa)
            {
                throw new Exception("Error INVOCANDO al servicio WSAA : " + excepcionAlInvocarWsaa.Message);
            }

            // PASO 4: Analizo el Login Ticket Response recibido del WSAA
            try
            {
                XmlLoginTicketResponse = new XmlDocument();
                XmlLoginTicketResponse.LoadXml(loginTicketResponse);

                this.UniqueId = UInt32.Parse(XmlLoginTicketResponse.SelectSingleNode("//uniqueId").InnerText);
                this.GenerationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//generationTime").InnerText);
                this.ExpirationTime = DateTime.Parse(XmlLoginTicketResponse.SelectSingleNode("//expirationTime").InnerText);
                this.Sign = XmlLoginTicketResponse.SelectSingleNode("//sign").InnerText;
                this.Token = XmlLoginTicketResponse.SelectSingleNode("//token").InnerText;
            }
            catch (Exception excepcionAlAnalizarLoginTicketResponse)
            {
                throw new Exception("Error ANALIZANDO el LoginTicketResponse : " + excepcionAlAnalizarLoginTicketResponse.Message);
            }
            return loginTicketResponse;
        }
    }
}
