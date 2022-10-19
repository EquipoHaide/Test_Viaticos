using Infraestructura.Transversal.Plataforma;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Nucleo.Firma
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
    }
}
