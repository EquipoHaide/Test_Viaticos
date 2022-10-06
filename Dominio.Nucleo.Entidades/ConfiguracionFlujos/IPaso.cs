using System;

namespace Dominio.Nucleo.Entidades
{
    public interface IPaso : IEntity , ISeguimiento
    {
      

        int IdFlujo { get; set; }
        int IdRolAutoriza { get; set; }

        int Orden { get; set; }
        /// <summary>
        /// Es un Enumerable
        /// 1 --> General
        /// 2 --> Especifico
        /// </summary>
        int TipoRol { get; set; }

        bool AplicaFirma { get; set; }

    }
}
