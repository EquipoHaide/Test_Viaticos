using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dominio.Nucleo;

namespace Dominio.Viaticos.Modelos
{
    public class FlujoTemp 
    {
        public int TipoEntePublico { get; set; }
        public double NivelEmpleado { get; set; }
        public List<Paso> Pasos { get; set; }
        public int TipoFlujo { get; set; }
        public string Descripcion  { get; set; }

        //[JsonConverter(typeof(InterfaceConverter<TipoEntePublico, ITipoEntePublico>))]
        //public ITipoEntePublico TipoEntePublico { get; set; }

        //[JsonConverter(typeof(InterfaceConverter<NivelEmpleado, INivelEmpleado>))]
        //public INivelEmpleado NivelEmpleado { get; set; }



        ////[JsonConverter(typeof(ListConverter<List<Paso>,List<IPaso>>))]

        //public List<Nucleo.IPaso> Pasos { get; set; }



        public bool IsValid()
        {
            return true;
        }

    }

  

    public class InterfaceConverter<M, I> : JsonConverter<I>
        where M : class, I

    {
        public override I Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<M>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, I value, JsonSerializerOptions options) { }
    }

    public class ListConverter<M,I> : JsonConverter<List<I>>
      
    {
        public override List<I> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<List<I>>(ref reader);
        }
        //public override List<M> Read(ref Utf8JsonReader reader, Type objectType, object existingValue,
        //                            JsonSerializer serializer)
        //{
        //    var jsonObj = serializer.Deserialize<List<M>>(reader);
        //    List<M> conversion = jsonObj.ConvertAll((x) => x as M);

        //    return conversion;
        //}


        public override void Write(Utf8JsonWriter writer, List<I> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }

  
}
