﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Nucleo.Firma
{
    public class RecursosFirma
    {
        public string Contrasena { get; set; }
        public ArchivoApi Certificado { get; set; }
        public ArchivoApi Llave { get; set; }
    }
}
