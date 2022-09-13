using System;
using System.Collections.Generic;
using System.Linq;
using Dominio.Nucleo;
using Infraestructura.Transversal.Plataforma;

using Infraestructura.Datos.Nucleo;

using Dominio.Nucleo.Repositorios;
using Dominio.Nucleo.Entidades;
using Infraestructura.Transversal.Plataforma.Extensiones;

namespace Aplicacion.Nucleo.ServicioConfiguracionFlujo
{
    public abstract class ServicioConfiguracionFlujoBase<TPaso, TFlujo> : IServicioConfiguracionFlujoBase<TPaso>
         where TPaso : IPaso
        where TFlujo : Dominio.Nucleo.Entidades.FlujoBase, new()
    {
        const string TAG = "Aplicacion.Nucleo.ServicioConfiguracionFlujo";

        public virtual Dominio.Nucleo.Servicios.ServicioConfiguracionFlujo.IServicioConfiguracionFlujoBase<TPaso> ServicioDominio { get; }


        public virtual IRepositorioConfiguracionFlujo<TFlujo> Repositorio { get; }
       

        /// <summary>
        /// PENDIENTE :
        /// </summary>
        /// <param name="flujos"></param>
        /// <returns></returns>
        public abstract bool ValidarPasos(IFlujo<TPaso> flujos);


        public Respuesta Consultar(IConsulta query, string subjectId)
        {

            return new Respuesta();
        }

        public Respuesta Crear(IFlujo<TPaso> flujo, string subjectId)
        {
            //Valida que el objeto no este vacio
            if (flujo == null)
                return new Respuesta("Es requerido un flujo de autorizacion ", TAG);

            if (!flujo.IsValid())
                return new Respuesta("El Flujo es invalido", TAG);

            if (flujo.TipoEntePublico == null || flujo.TipoEntePublico.Id <= 0)
                return new Respuesta("El Tipo de Ente es requerido", TAG);

            if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
                return new Respuesta("La lista de pasos es requerida.", TAG);

            if (flujo.TipoFlujo <= 0)
                return new Respuesta("El tipo de flujo es requerido", TAG);

            if (flujo.TipoFlujo == (int)TipoFlujo.Particular)
            {
                if (flujo.NivelEmpleado == null)
                    return new Respuesta("El nivel de empleado es requerido", TAG);

                if (flujo.NivelEmpleado.Nivel.ToString().IsNullOrEmptyOrWhiteSpace())
                    return new Respuesta("El nivel del empleado es requerido para un flujo particular.", TAG);
            }

            //CONSULTA AL REPOSITORIO
            //BUSCAR SI YA EXISTE UN FLUJO PREDETERMINADO ANTES QUE UN FLUJO PARTICULAR 
            //            
            var esPredertiminado = Repositorio.Try(r => r.ExisteFlujoPredeterminado(flujo.TipoEntePublico.Id));


            if (esPredertiminado.EsError)
            {
                return esPredertiminado.ErrorBaseDatos(TAG);
            }


            //CONSULTA AL REPOSITORIO 
            //BUSCA SI EXITE ALGUN FLUJO PARTICULAR CON EL MISMO NIVEL 
            //
            var esNivelRepetido = Repositorio.Try(r => r.ExisteNivelRepetido(flujo.TipoEntePublico.Id, flujo.NivelEmpleado.Nivel));


            if (esNivelRepetido.EsError)
            {
                return esPredertiminado.ErrorBaseDatos(TAG);
            }            

            var respuesta = ServicioDominio.Crear(flujo, esPredertiminado.Contenido, esNivelRepetido.Contenido, subjectId);

            if (respuesta.EsExito)
            {
                if (this.ValidarPasos(flujo))
                {
                    //Todo: Aqui todo lo que sigue 
                    //---------------PENDIENTE------------------
                    /*Temporal ------->  */
                    var flujoBase = new Dominio.Nucleo.Entidades.FlujoBase();
                    TFlujo entity = new TFlujo()
                    {
                        Id = respuesta.Contenido.Id,
                        //IdNivelEmpleado = respuesta.Contenido.NivelEmpleado.Id
                    };
                    //PENDIENTE: ESTO ME DA UN ERROR AL REALIZAR EL CASTEO, LO QUE SE TRATA DE CONVERTIR ES UN FLUJO BASE A UN FLUJO BASE VIATICOS
                    //Repositorio.Add((TFlujo)flujoBase);
                    //var flujoAguardar = (TFlujo)respuesta.Contenido;
                    Repositorio.Add((TFlujo)respuesta.Contenido);

                    //====>> De esta manera no me da error, pero lo que se guarda es un base.
                    Repositorio.Add(entity);

                    var save = Repositorio.Try(r => r.Save());

                    if (save.EsError)
                    {
                        //App.GetLogger().Log.Information(save.ExcepcionInterna, save.Mensaje);
                        return save.ErrorBaseDatos(TAG);
                    }

                    return new Respuesta();

                }

                return new Respuesta(respuesta.Mensaje, TAG);
            }

            return new Respuesta(respuesta.Mensaje, TAG);

        }


        public Respuesta Modificar(IFlujo<TPaso> flujo,  string subjectId)
        {
            //Valida que el objeto no este vacio
            if (flujo == null)
                return new Respuesta("Es requerido un flujo de autorizacion ", TAG);

            if (!flujo.IsValid())
                return new Respuesta("El Flujo es invalido", TAG);

            if (flujo.Pasos == null || flujo.Pasos.Count() <= 0)
                return new Respuesta("La lista de pasos es requerida.", TAG);

            if (flujo.TipoFlujo.ToString() == null)
                return new Respuesta("El tipo de flujo es requerido", TAG);

            if (flujo.TipoFlujo == (int)TipoFlujo.Particular)
            {
                if (flujo.NivelEmpleado == null)
                    return new Respuesta("El nivel de empleado es requerido", TAG);

                if (flujo.NivelEmpleado.Nivel.ToString().IsNullOrEmptyOrWhiteSpace())
                    return new Respuesta("El nivel del empleado es requerido para un flujo particular.", TAG);
            }

            var flujoOriginal = Repositorio.Try(r => r.Get(g => g.Id == flujo.Id));
            if (flujoOriginal.EsError) return flujoOriginal.ErrorBaseDatos(TAG);

            //var respuesta = Servicio.Modificar(grupo.ToEntity<Entidades.Grupo>(grupoOriginal.Contenido), mismoNombre.Contenido, permisos.Contenido, subjectId);

            var respuesta = ServicioDominio.Modificar(flujo, /*esPredertiminado.Contenido*/ false, /*esNivelRepetido.Contenido*/ false, subjectId);


            if (respuesta.EsExito)
            {
                if (this.ValidarPasos(flujo))
                {
                    var save = Repositorio.Try(r => r.Save());
                    if (save.EsError) return save.ErrorBaseDatos(TAG);

                    return new Respuesta();
                }
                
            }
            else
            {
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);
            }

            return new Respuesta(respuesta.Mensaje, respuesta.TAG);

        }

        public Respuesta Eliminar(List<IFlujo<TPaso>> flujo, RepositorioConfiguracionFlujo<Dominio.Nucleo.Entidades.FlujoBase> repositorioConfiguracion, string subjectId)
        {
            IEnumerable<Dominio.Nucleo.Entidades.FlujoBase> lista = null;
           // var flujosOriginales = Repositorio.Try(r => r.ObtenerFlujos(lista, subjectId));
 
            //if (flujosOriginales.EsError)
            //    return flujosOriginales.ErrorBaseDatos();

           
            var respuesta = ServicioDominio.Eliminar(null, subjectId);

            if (respuesta.EsExito)
            {
                //Repositorio.Remove( ----> PENDIENTE <----);
                try
                {
                    return new Respuesta();
                }
                catch (Exception ex)
                {

                    return new Respuesta(ex, /*R.strings.ErrorConexionBaseDeDatos*/ "Error en la conexion en Base de Datos", TAG);
                }
            }
            else
            {
                return new Respuesta(respuesta.Mensaje, respuesta.TAG);
            }
        }




    }
}
