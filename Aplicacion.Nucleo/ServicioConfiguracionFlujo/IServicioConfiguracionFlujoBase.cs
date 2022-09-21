using System;
using System.Collections.Generic;
using Dominio.Nucleo;
using Dominio.Nucleo.Repositorios;
using Infraestructura.Datos.Nucleo;
using Infraestructura.Transversal.Plataforma;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public interface IServicioConfiguracionFlujoBase<TFlujo,TPaso>
        where TFlujo : class, IFlujo<TPaso>
        where TPaso : class, IPaso
    {

        Respuesta<bool> ValidarPasos(TFlujo flujos);

        //las entidades de los deferentes negocios(viaticos, compras, etc), deberian de heredar de mi entidad principal Flujo
        //public Respuesta<bool> Crear(List<IFlujo<TPaso>> flujos, IRepositorioConfiguracionFlujo<Flujo> repositorioConfiguracionFlujo );

        //bool ValidarPasos();ConsultaPaginada<IPermisoModel>
        public Respuesta<ConsultaPaginada<TFlujo>> Consultar(TFlujo query, string subjectId);

        public Respuesta Crear(TFlujo flujo,  string subjectId);

        public Respuesta Modificar(TFlujo flujo, string subjectId);

        public Respuesta Eliminar(TFlujo flujo, string subjectId);
        


        }
}
