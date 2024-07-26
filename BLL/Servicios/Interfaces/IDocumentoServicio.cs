using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios.Interfaces
{
    public interface IDocumentoServicio
    {
        Task<IEnumerable<DocumentoDto>> ObtenerTodos();
        Task<DocumentoDto> Agregar(DocumentoDto modeloDto);
        Task Actualizar(DocumentoDto modeloDto);
        Task Remover(int id);
        Task<IEnumerable<DocumentoDto>> ObtenerDocumentosUsuario(string documentoUsuarioId);
        Task<DocumentoUsuarioDto> GetDocumentoUsuarioId(int usaurioId);
    }
}
