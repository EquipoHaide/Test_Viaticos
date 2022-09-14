using Dominio.Nucleo;
using AutoMapper;
using MicroServices.Platform.Extensions;

namespace Dominio.Viaticos.Modelos
{
    public class ViaticosProfileMapper
    {
        public static void CreateMap(MainMapper mapper)
        {
            var perfil = mapper.GetProfile("Viaticos");
            if (perfil == null) return;

            perfil.ValueTransformers.Add<string>(val => val.IsNullOrEmptyOrWhiteSpace() ? null : val.Trim());

            perfil.CreateMap<Entidades.FlujoViaticos, Modelos.FlujoViaticos>().ReverseMap();

            perfil.CreateMap<Modelos.FlujoViaticos, Entidades.FlujoViaticos>()
                  .ForMember(dest => dest.IdNivelEmpleado, opts => opts.MapFrom(src => src.NivelEmpleado.Id))
                  .ForMember(dest => dest.IdTipoEntePublico, opts => opts.MapFrom(src => src.TipoEntePublico.Id)).ReverseMap();



            perfil.CreateMap<Modelos.ModeloFlujo<PasoViatico>, Modelos.FlujoViaticos>().ReverseMap();

        }
    }
}
