using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dominio.Nucleo.Firma
{
    public class ArchivoApi
    {
        public int IdArchivoAPI { get; set; }

        public string Nombre { get; set; }

        public string Extension { get; set; }

        public string Peso { get; set; }

        public string MimeType { get; set; }

        public Stream Archivo { get; set; }

        public bool EsParteDeConjunto { get; set; }

        public string GuidConjunto { get; set; }

        public string Error { get; set; }

        public bool Completado { get; set; }
    }
}
