﻿using AutoMapper;

namespace GestionApiTareas.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {   // -------Origen, Destino
            //CreateMap<AutorCreacionDTO, Autor>();
            //CreateMap<Autor, AutorDTO>();
            //CreateMap<Autor, AutorDTOConLibros>().ForMember(autorDTO => autorDTO.Libros, opciones => opciones.MapFrom(MapAutorDTOLibros));
            //CreateMap<LibroPatchDTO, Libro>().ReverseMap();
        }

        //private List<AutorLibro> MapAutoresLibros(LibroCreacionDTO libroCreacionDTO, Libro libro)
        //{
        //    var result = new List<AutorLibro>();

        //    if (libroCreacionDTO.IdAutores is null) { return result; }

        //    foreach (var autorId in libroCreacionDTO.IdAutores)
        //    {
        //        result.Add(new AutorLibro() { IdAutor = autorId });
        //    }

        //    return result;

        //}
    }
}