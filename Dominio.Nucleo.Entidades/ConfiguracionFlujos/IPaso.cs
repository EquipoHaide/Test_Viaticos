using System;

namespace Dominio.Nucleo.Entidades
{
    public interface IPaso : IEntity
    {
        int IdRol { get; set; }

        int IdConfiguracionFlujo { get; set; }

        int Orden { get; set; }
        /// <summary>
        /// Es un Enumerable
        /// 1 --> General
        /// 2 --> Especifico
        /// </summary>
        int TipoRol { get; set; }

        bool EsFirma { get; set; }

    }
}
