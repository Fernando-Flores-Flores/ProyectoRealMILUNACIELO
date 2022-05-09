using AutoMapper;
using BackMiLunaCielo.Models;
using BackMiLunaCielo.Models.Dtos;

namespace BackMiLunaCielo.UsuariosMapper
{
    public class UsuariosMappers:Profile
    {

        //
        //Aqui vnculamos la clase y el dto
        public UsuariosMappers()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }

    }
}
