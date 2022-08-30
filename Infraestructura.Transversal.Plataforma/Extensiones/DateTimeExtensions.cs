using System;

namespace Infraestructura.Transversal.Plataforma.Extensiones
{
    public static class DateTimeExtensions
    {

        public static string FechaDiaMesAnio(this DateTime fecha)
        {
            string formatearFecha = fecha.ToString("dd/MM/yyyy");

            return formatearFecha;
        }

        /// <summary>
        /// Retorna el siguiente dia habil
        /// Omite los dias sabado y domingo en caso que  sean iguales a las fecha parametrizada y retornan el lunes mas cercano
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static DateTime ObtenerSiguienteDiaHabil(this DateTime fecha)
        {
            var dia = fecha.DayOfWeek;

            if (dia == DayOfWeek.Saturday)
                return fecha.AddDays(2);

            if (dia == DayOfWeek.Sunday)
                return fecha.AddDays(1);

            return fecha;
        }

        public static DateTime SumarDiasHabiles(this DateTime fecha, int numeroDias)
        {
            for (int i = 0; i < numeroDias; i++)
            {
                fecha = fecha.AddDays(1);

                while (fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday)
                {
                    fecha = fecha.AddDays(1);
                }
            }
            return fecha;
        }


    }
}
