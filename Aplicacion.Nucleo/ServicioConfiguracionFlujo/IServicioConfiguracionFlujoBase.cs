using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Entidades;
using Dominio.Nucleo.Repositorios;
using Infraestructura.Datos.Nucleo;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TPaso>
    where TPaso : IPaso
    {

        bool ValidarPasos(IFlujo<TPaso> flujos);

        //las entidades de los deferentes negocios(viaticos, compras, etc), deberian de heredar de mi entidad principal Flujo
        //public Respuesta<bool> Crear(List<IFlujo<TPaso>> flujos, IRepositorioConfiguracionFlujo<Flujo> repositorioConfiguracionFlujo );

        //bool ValidarPasos();
        public Respuesta Consultar(IConsulta query, string subjectId);

        public Respuesta Crear(IFlujo<TPaso> flujo, RepositorioConfiguracionFlujo<Dominio.Nucleo.Entidades.FlujoBase> repositorioConfiguracion, string subjectId);

        public Respuesta Modificar(IFlujo<TPaso> flujo, RepositorioConfiguracionFlujo<Dominio.Nucleo.Entidades.FlujoBase> repositorioConfiguracion, string subjectId);


    }
}
