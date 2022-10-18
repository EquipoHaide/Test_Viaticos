using Infraestructura.Transversal.Plataforma;
using System.IO;

namespace Infraestructura.Transversal.ServiciosExternos.FirmaElectronica
{
    public interface IServicioFirmaElectronica
    {
        /// <summary>
        /// Método que realiza firmado
        /// </summary>
        Respuesta<string> Firmar(byte[] key, string pass, string cadenaOriginal, string certificadoBase64);

        /// <summary>
        /// Método que valida firma obtenida
        /// </summary>
        Respuesta ValidarFirma(string firmaElectronica, string cadenaOriginal, string certificadoBase64);

        /// <summary>
        /// Método que obtención del data del certificado dado
        /// </summary>
        Respuesta<Modelos.VerModeloCertificado> ObtenerDatosCertificado(Stream certificado);
    }
}
