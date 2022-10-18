using Aplicacion.Nucleo;
using FirmaElectronica.DTOs;
using FirmaElectronica.Excepciones;
using FirmaElectronica.Fabricas;
using FirmaElectronica.ServiciosExternos;
using Infraestructura.Transversal.Plataforma;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace Infraestructura.Transversal.ServiciosExternos.FirmaElectronica
{
    public class ServicioFirmaElectronica: IServicioFirmaElectronica
    {
        static readonly string TAG;// = "Infraestructura.Transversal.ServiciosExternos.FirmaElectronica";
        public IAplicacion App { get; set; }
        //Por si cambia el namespace
        static ServicioFirmaElectronica() 
        {
            TAG = typeof(ServicioFirmaElectronica).Namespace;
        }

        public ServicioFirmaElectronica(IAplicacion app) 
        {
            App = app;
        }

        /// <summary>
        /// Método que realiza firmado
        /// </summary>
        public Respuesta<string> Firmar(byte[] key, string pass, string cadenaOriginal, string certificadoBase64) 
        {
            try
            {
                DTODatosInicializacion usuarioSAT = ObtenerDatosInicializacion();

                var firmadorSha = FabricaFirmaElectronica.ObtenerServicioFirma(key, pass, usuarioSAT);

                string sello = firmadorSha.Firmar(cadenaOriginal, TiposOrigenCertificado.Base64, certificadoBase64);

                return new Respuesta<string>(TAG) { Contenido = sello };
            }
            catch (FaultException<global::FirmaElectronica.ServiciosExternos.Certificados.ErrorGenerico> ex)
            {
                return new Respuesta<string>(ex.Detail.Detalles, TAG);
            }
            catch (FaultException<global::FirmaElectronica.ServiciosExternos.Certificados.ErrorControlado> ex)
            {
                return new Respuesta<string>(ex.Detail.Detalles, TAG);
            }
            catch (ValidacionExcepcion ex)
            {
                return new Respuesta<string>(ex.Message, TAG);
            }
            catch (Exception ex)
            {
                return new Respuesta<string>(ex.Message, TAG);
            }
        }

        /// <summary>
        /// Método que valida firma obtenida
        /// </summary>
        public Respuesta ValidarFirma(string firmaElectronica, string cadenaOriginal, string certificadoBase64)
        {
            try
            {
                DTODatosInicializacion usuarioSAT = ObtenerDatosInicializacion();

                var validadorSha = FabricaFirmaElectronica.ObtenerServicioValidadorFirmas(usuarioSAT);

                validadorSha.ValidarFirma(TiposOrigenCertificado.Base64, certificadoBase64, firmaElectronica, cadenaOriginal);

                return new Respuesta();
            }
            catch (FaultException<global::FirmaElectronica.ServiciosExternos.Certificados.ErrorGenerico> ex)
            {
                return new Respuesta(ex.Detail.Detalles, TAG);
            }
            catch (FaultException<global::FirmaElectronica.ServiciosExternos.Certificados.ErrorControlado> ex)
            {
                return new Respuesta(ex.Detail.Detalles, TAG);
            }
            catch (ValidacionExcepcion ex)
            {
                return new Respuesta(ex.Message, TAG);
            }
            catch (Exception ex)
            {
                return new Respuesta(ex.Message, TAG);
            }
        }

        /// <summary>
        /// Método que obtención del data del certificado dado
        /// </summary>
        public Respuesta<Modelos.VerModeloCertificado> ObtenerDatosCertificado(Stream certificado)
        {
            try
            {
                certificado.Position = 0;
                var archivo = certificado;
                byte[] dataCert = new byte[archivo.Length];
                archivo.Read(dataCert, 0, dataCert.Length);

                X509Certificate2 certificate = new X509Certificate2(dataCert);

                string[] subject = certificate.Subject.Split(',');

                var dataCertificado = new Modelos.VerModeloCertificado();

                foreach (string strVal in subject)
                {
                    string valor = strVal.Trim();

                    if (valor.StartsWith("OID.2.5.4.45="))
                    {
                        string valorDos = valor.Replace("OID.2.5.4.45=", "");
                        dataCertificado.Rfc = valorDos.Substring(0, valorDos.IndexOf('/') >= 0 ? valorDos.IndexOf('/') : valorDos.Length).Trim();
                    }

                    if (valor.StartsWith("2.5.4.45="))
                    {
                        string valorDos = valor.Replace("2.5.4.45=", "");
                        dataCertificado.Rfc = valorDos.Substring(0, valorDos.IndexOf('/') >= 0 ? valorDos.IndexOf('/') : valorDos.Length).Trim();
                    }

                    if (valor.StartsWith("CN="))
                    {
                        string valorDos = valor.Replace("CN=", "");
                        dataCertificado.NombreCompleto = valorDos.Trim();
                    }
                }

                return new Respuesta<Modelos.VerModeloCertificado>(dataCertificado);
            }
            catch (Exception ex)
            {
                return new Respuesta<Modelos.VerModeloCertificado>(ex.Message, TAG);
            }
        }

        /// <summary>
        /// Carga los datos para que la dll de firma pueda utilizar los servicios OSCP, no se inicaliza en el constructor estático para evitar tener cargadas las credenciales en memoria.
        /// </summary>
        /// <returns></returns>
        private DTODatosInicializacion ObtenerDatosInicializacion()
        {
            var configuration = this.App.GetConfiguration();
            var seccionOCSP = configuration.GetSection("ConexionOCSP");

            var usuario = seccionOCSP.GetSection("Usuario").Value;
            var contrasena = seccionOCSP.GetSection("Contrasena").Value;
            var esHashContrasena = seccionOCSP.GetSection("EsHashContrasena").Value;
            var urlEndpointOCSP = seccionOCSP.GetSection("UrlEndpointOCSP").Value;

            var usuarioSAT = new DTODatosInicializacion()
            {
                Usuario = usuario,
                Contrasena = contrasena,
                EsHashContrasena = bool.TryParse(esHashContrasena, out bool result) && result,
                UrlEndpointOCSP = urlEndpointOCSP
            };
            return usuarioSAT;
        }
    }
}
