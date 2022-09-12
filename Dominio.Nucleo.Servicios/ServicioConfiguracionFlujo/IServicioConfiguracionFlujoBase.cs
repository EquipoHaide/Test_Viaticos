using System;
using System.Collections.Generic;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TPaso> where TPaso : IPaso
    {

        public Respuesta<IFlujo<TPaso>> Crear(IFlujo<TPaso> flujos, bool esPredeterminado, bool esNivelRepetido, string subjectId);


        public Respuesta<IFlujo<TPaso>> Modificar(IFlujo<TPaso> flujos, bool esPredeterminado, bool esNivelRepetido, string subjectId);

        //  IEnumerable<TEntidad> ObtenerFlujos(IEnumerable<TEntidad> flujo, string subjectId);

        public Respuesta<List<IFlujo<TPaso>>> Eliminar(IFlujo<TPaso> flujos,string subjectId);


    }
}
