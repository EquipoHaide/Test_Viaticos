﻿using System;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Entidades
{
    public class Paso : IPaso
    {
        public int Id { get; set ; }
        public int Orden { get; set ; }
        public int Rol { get; set ; }
        public int TipoRol { get; set ; }
        public bool EsFirma { get; set ; }

        public bool IsValid()
        {
            return true;
        }
    }
}
