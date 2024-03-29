﻿using System;
using System.Collections.Generic;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma;

namespace Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TPaso,TEntidad>
        where TPaso : IPaso
        where TEntidad : class
    {

        public TEntidad ObtenerEntidad(IFlujo<TPaso> flujo);

        public Respuesta<TEntidad> Crear(IFlujo<TPaso> flujos, bool esPredeterminado, bool esNivelRepetido, string subjectId);

        public Respuesta<IFlujo<TPaso>> Modificar(IFlujo<TPaso> flujos, bool esPredeterminado, bool esNivelRepetido, string subjectId);

        //  IEnumerable<TEntidad> ObtenerFlujos(IEnumerable<TEntidad> flujo, string subjectId);

        public Respuesta<Dominio.Nucleo.Entidades.IFlujo> Eliminar(IFlujo<TPaso> flujos,string subjectId);


    }
}
