using Dominio.Nucleo;
using AutoMapper;
using MicroServices.Platform.Extensions;

namespace Dominio.Seguridad.Modelos
{
    public class SeguridadProfileMapper
    {
        public static void CreateMap(MainMapper mapper)
        {
            var perfil = mapper.GetProfile("Seguridad");
            if (perfil == null) return;

            perfil.ValueTransformers.Add<string>(val => val.IsNullOrEmptyOrWhiteSpace() ? null : val.Trim());

            perfil.CreateMap<Entidades.RecursoGrupo, Modelos.RecursoDeGrupo>().ReverseMap();
            perfil.CreateMap<Modelos.RecursoDeGrupo, Modelos.RecursoDeGrupo>();
            perfil.CreateMap<Entidades.RecursoRol, Modelos.RecursoDeRol>().ReverseMap();
            perfil.CreateMap<Modelos.RecursoDeRol, Modelos.RecursoDeRol>();
            perfil.CreateMap<Entidades.RecursoAccion, Modelos.RecursoDeAccion>().ReverseMap();
            perfil.CreateMap<Modelos.RecursoDeAccion, Modelos.RecursoDeAccion>();
            perfil.CreateMap<Entidades.Usuario, Modelos.Usuario>().ReverseMap();
            perfil.CreateMap<Entidades.Grupo, Modelos.Grupo>().ReverseMap();
            perfil.CreateMap<Entidades.Rol, Modelos.Rol>().ReverseMap();
            perfil.CreateMap<Entidades.Modulo, Modelos.Modulo>().ReverseMap();
            perfil.CreateMap<Entidades.OpcionModulo, Modelos.OpcionModulo>().ReverseMap();
            perfil.CreateMap<Entidades.Accion, Modelos.Accion>().ReverseMap();
            perfil.CreateMap<Entidades.Acceso, Modelos.Acceso>()
                  .ForMember(dest => dest.Accion, opts => opts.MapFrom(src => src.Accion.Nombre)).ReverseMap();
            perfil.CreateMap<Modelos.Acceso, Entidades.Acceso>()
                .ForMember(dest => dest.Accion, opt => opt.Ignore()).ReverseMap();


        }
    }
}
