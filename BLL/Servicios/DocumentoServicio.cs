using AutoMapper;
using BLL.Servicios.Interfaces;
using Data;
using Data.Interfaces.IRepositorio;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Servicios
{
    public class DocumentoServicio : IDocumentoServicio
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IMapper _mapper;
        private readonly UserManager<UsuarioAplicacion> _userManager;
        private readonly ApplicationDbContext _db;

        public DocumentoServicio(UserManager<UsuarioAplicacion> userManager, IUnidadTrabajo unidadTrabajo, IMapper mapper, ApplicationDbContext db)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _userManager = userManager;
            _db = db;
        }

        public async Task<DocumentoDto> Agregar(DocumentoDto modeloDto)
        {
            try
            {
                Documento documento = new Documento
                {
                    Descripcion = modeloDto.Descripcion,
                    url = modeloDto.url,
                    TipoDocumentoId = modeloDto.TipoDocumentoId,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };
                await _unidadTrabajo.Documento.Agregar(documento);
                await _unidadTrabajo.Guardar();

                if (documento.Id == 0)
                {
                    throw new TaskCanceledException("El medico no se pudo crear");
                }

                return _mapper.Map<DocumentoDto>(documento);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task Actualizar(DocumentoDto modeloDto)
        {
            try
            {
                var documentoDb = await _unidadTrabajo.Documento.ObtenerPrimero(e => e.Id == modeloDto.Id);
                if (documentoDb == null)
                {
                    throw new TaskCanceledException("La medico no existe");
                }

                documentoDb.Descripcion = modeloDto.Descripcion;
                documentoDb.url = modeloDto.url;
                documentoDb.TipoDocumentoId = modeloDto.TipoDocumentoId;
                documentoDb.FechaActualizacion = DateTime.Now;

                _unidadTrabajo.Documento.Actualizar(documentoDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<DocumentoDto>> ObtenerTodos()
        {
            try
            {
                var lista = await _unidadTrabajo.Documento.ObtenerTodos(incluirPropiedades: "tipoDocumento",
                                    orderBy: e => e.OrderBy(e => e.Descripcion));

                return _mapper.Map<IEnumerable<DocumentoDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<DocumentoDto>> ObtenerDocumentosUsuario(string documentoUsuarioId)
        {
            try
            {
              
                var lista = await _unidadTrabajo.Documento.ObtenerTodos(x => x.DocumentoUsaurioId.ToString() == documentoUsuarioId, incluirPropiedades: "tipoDocumento",
                                    orderBy: e => e.OrderBy(e => e.Descripcion));

                return _mapper.Map<IEnumerable<DocumentoDto>>(lista);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Remover(int id)
        {
            try
            {
                var documentoDb = await _unidadTrabajo.Documento.ObtenerPrimero(e => e.Id == id);
                if (documentoDb == null)
                {
                    throw new TaskCanceledException("El documento no existe");
                }

                _unidadTrabajo.Documento.Remover(documentoDb);
                await _unidadTrabajo.Guardar();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DocumentoUsuarioDto> GetDocumentoUsuarioId(int usaurioId)
        {
            try
            {
                var usaurio = await _db.DocumentosUsuarios.Where(x => x.UsuarioId == usaurioId).FirstOrDefaultAsync();
                if (usaurio == null)
                {
                    throw new TaskCanceledException("Usuario no tiene documentos agregados");
                }

                return _mapper.Map<DocumentoUsuarioDto>(usaurio);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
