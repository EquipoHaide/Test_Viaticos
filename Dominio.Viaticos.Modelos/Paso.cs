﻿using System;
using System.Text.Json.Serialization;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Modelos
{
    public class Paso : IPaso
    {
        public int Id       { get; set; }
        public int Orden    { get; set; }
        public int Rol      { get; set; }
        public int TipoRol  { get; set; }
        public bool EsFirma { get; set; }

        public bool IsValid()
        {
            return true; 
        }
    }
}
