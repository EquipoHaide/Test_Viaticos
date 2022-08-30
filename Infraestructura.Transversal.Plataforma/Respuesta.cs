using System;

namespace Infraestructura.Transversal.Plataforma
{
    public class Respuesta<TResult>
    {
        public string TAG { get; set; }
        public EstadoProceso Estado { get; set; }
        public string Mensaje { get; set; }
        public TResult Contenido { get; set; }
        public Exception ExcepcionInterna { get; set; }


        public Respuesta(string tag, EstadoProceso estado = EstadoProceso.Exitoso)
        {
            this.TAG = tag;
            this.Estado = estado;
        }
        public Respuesta(TResult contenido, EstadoProceso estado = EstadoProceso.Exitoso) : this(null, estado)
        {
            this.Contenido = contenido;
        }
        public Respuesta(string mensaje, string tag, EstadoProceso estado = EstadoProceso.Fallido) : this(tag, estado)
        {
            Mensaje = mensaje;
        }
        public Respuesta(Exception excepcion, string tag, EstadoProceso estado = EstadoProceso.Fatal) : this(excepcion?.Message, tag, estado)
        {
            this.ExcepcionInterna = excepcion;
        }
        public Respuesta(Exception excepcion, string mensaje, string tag, EstadoProceso estado = EstadoProceso.Fatal) : this(excepcion, tag, estado)
        {
            this.Mensaje = mensaje;
        }
        public bool EsExito
        {
            get
            {
                return this.Estado == EstadoProceso.Exitoso;
            }
        }
        public bool EsError
        {
            get
            {
                return !EsExito;
            }
        }


        public Respuesta Error(string mensaje = null, string tag = null)
        {
            return new Respuesta(ExcepcionInterna, mensaje ?? Mensaje ?? "Ocurrio un error. [DB500]", tag ?? TAG);
        }
        public Respuesta<T> Error<T>(string mensaje = null, string tag = null)
        {
            return new Respuesta<T>(ExcepcionInterna, mensaje ?? Mensaje ?? "Ocurrio un error. [DB500]", tag ?? TAG);
        }
        public Respuesta ErrorBaseDatos(string tag = null)
        {
            return new Respuesta(ExcepcionInterna, "Ocurrio un error. [DB500]", tag ?? TAG);
        }
        public Respuesta<T> ErrorBaseDatos<T>(string tag = null)
        {
            return new Respuesta<T>(ExcepcionInterna, "Ocurrio un error. [DB500]", tag ?? TAG);
        }
    }

    public class Respuesta
    {
        private DateTime now;

        public string TAG { get; set; }
        public EstadoProceso Estado { get; set; }
        public string Mensaje { get; set; }
        public Exception ExcepcionInterna { get; set; }

        public Respuesta(EstadoProceso estado = EstadoProceso.Exitoso)
        {
            this.Estado = estado;
        }
        public Respuesta(string tag, EstadoProceso estado = EstadoProceso.Exitoso)
        {
            this.TAG = tag;
            this.Estado = estado;
        }
        public Respuesta(string mensaje, string tag, EstadoProceso estado = EstadoProceso.Fallido) : this(tag, estado)
        {
            Mensaje = mensaje;
        }
        public Respuesta(Exception excepcion, string tag, EstadoProceso estado = EstadoProceso.Fatal) : this(excepcion?.Message, tag, estado)
        {
            this.ExcepcionInterna = excepcion;
        }
        public Respuesta(Exception excepcion, string mensaje, string tag, EstadoProceso estado = EstadoProceso.Fatal) : this(excepcion, tag, estado)
        {
            this.Mensaje = mensaje;
        }

        public Respuesta(DateTime now)
        {
            this.now = now;
        }

        public bool EsExito
        {
            get
            {
                return this.Estado == EstadoProceso.Exitoso;
            }
        }
        public bool EsError
        {
            get
            {
                return !EsExito;
            }
        }

        public Respuesta Error(string mensaje = null, string tag = null)
        {
            return new Respuesta(ExcepcionInterna, mensaje ?? Mensaje ?? "Ocurrio un error. [DB500]", tag ?? TAG);
        }
        public Respuesta<T> Error<T>(string mensaje = null, string tag = null)
        {
            return new Respuesta<T>(ExcepcionInterna, mensaje ?? Mensaje ?? "Ocurrio un error. [DB500]", tag ?? TAG);
        }
        public Respuesta ErrorBaseDatos(string tag = null)
        {
            return new Respuesta(ExcepcionInterna, Mensaje ?? "Ocurrio un error. [DB500]", tag ?? TAG);
        }
        public Respuesta<T> ErrorBaseDatos<T>(string tag = null)
        {
            return new Respuesta<T>(ExcepcionInterna, Mensaje ?? "Ocurrio un error. [DB500]", tag ?? TAG);
        }
    }
}