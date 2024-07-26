using AutoMapper;
using Models.DTOs;
using Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Documento, DocumentoDto>()
                .ForMember(d => d.tipoDocumento, td => td.MapFrom(r => r.tipoDocumento.Descripcion))
                .ForMember(d => d.FechaCreacion, td => td.MapFrom(r => r.FechaCreacion.ToString("dd/MM/yyyy hh:mm tt")));

            CreateMap<DocumentoUsuario, DocumentoUsuarioDto>();

        }
    }
}
